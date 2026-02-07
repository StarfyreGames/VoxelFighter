using System;
using System.Collections.Generic;
using System.IO;
using Gun.Persistence;
using Persistence.Seeders;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

namespace Persistence.Scripts
{
    public class SaveSlotManager : MonoBehaviour
    {
        public static SQLiteConnection Database { get; private set; }

        private static readonly List<ISaveSlotSeeder> Seeders = new()
        {
            new CreateTablesSeeder(),
            new PlayerShipSeeder()
        };

        private static SaveSlotManager Instance { get; set; }

        // A flag that will reset the database in dev builds 
        [SerializeField] public bool resetDbOnStartInDevMode = true;

        private void Awake()
        {
            // We can singleton this champ
            if (Instance != null && Instance != this) Destroy(this);
            else Instance = this;

#if UNITY_EDITOR || DEVELOPMENT_BUILD
            // Load a testing save file for debugging
            SetActiveSaveSlot("TestingSaveSlot");
#endif
        }

        public void SetActiveSaveSlot(string saveSlotName)
        {
            var path = Path.Combine(Application.persistentDataPath, saveSlotName) + ".db";
            var isNewSaveSlot = !File.Exists(path);

#if UNITY_EDITOR || DEVELOPMENT_BUILD
            // Reset the database every time we load but only in dev mode
            if (resetDbOnStartInDevMode)
            {
                File.Delete(path);
                isNewSaveSlot = true;
            }
#endif

            Database = new SQLiteConnection(path, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

            if (isNewSaveSlot) SeedNewSaveSlot(Database);
        }

        private void SeedNewSaveSlot(SQLiteConnection connection)
        {
            Database.RunInTransaction(() =>
                Seeders.ForEach(seeder => seeder.Seed(connection))
            );
        }
    }
}