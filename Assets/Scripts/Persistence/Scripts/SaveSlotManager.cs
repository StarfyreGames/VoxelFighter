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
            new PlayerInitialWeaponLayoutSeeder()
        };

        private static SaveSlotManager Instance { get; set; }

        private void Awake()
        {
            // We can singleton this champ
            if (Instance != null && Instance != this) Destroy(this);
            else Instance = this;
         
            // Just so that we have something to test with
            // in reality this would be selected in the startup menu
            SetActiveSaveSlot("TestingSaveSlot");
        }

        public void SetActiveSaveSlot(string saveSlotName)
        {
            var path = Path.Combine(Application.persistentDataPath, saveSlotName) + ".db";

            var isNewSaveSlot = !File.Exists(path);
            Database = new SQLiteConnection(path, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
            
            if (isNewSaveSlot) SeedNewSaveSlot(Database);
        }

        private void SeedNewSaveSlot(SQLiteConnection connection)
        {
            Seeders.ForEach(seeder => seeder.Seed(connection));
        }
    }
}