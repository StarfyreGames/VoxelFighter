using System.Collections.Generic;
using Gun.Persistence;
using UnityEngine;

namespace Gun.Model
{
    [CreateAssetMenu]
    public class WeaponBlueprintCatalogue : ScriptableObject
    {
        public WeaponBlueprint pulseCannonBase;
        public WeaponBlueprint spreadShotBase;
        public WeaponBlueprint lightningBase;
        public WeaponBlueprint laserBase;
        /*Added by Russell to test  -- may need to rename these appropriately
        public WeaponBlueprint pulseCannonUpgradeA;
        public WeaponBlueprint pulseCannonUpgradeB;
        public WeaponBlueprint pulseCannonUpgradeC;        
        */
        private Dictionary<string, WeaponBlueprint> _blueprintsByName;
        
        public WeaponBlueprint FindByName(string weaponName)
        {
            if (_blueprintsByName != null) 
                return _blueprintsByName[weaponName];

            _blueprintsByName = new Dictionary<string, WeaponBlueprint>();
            PopulateLookup(pulseCannonBase);
            PopulateLookup(spreadShotBase);
            PopulateLookup(lightningBase);
            PopulateLookup(laserBase);

            //Added by Russell to test\\
            /*PopulateLookup(pulseCannonUpgradeA);
            PopulateLookup(pulseCannonUpgradeB);
            PopulateLookup(pulseCannonUpgradeC);*/

            return _blueprintsByName[weaponName];
        }

        private void PopulateLookup(WeaponBlueprint blueprint)
        {
            while (blueprint != null)
            {
                _blueprintsByName.Add(blueprint.weaponName, blueprint);
                blueprint = blueprint.nextUpgrade;
            }
        }
    }
}