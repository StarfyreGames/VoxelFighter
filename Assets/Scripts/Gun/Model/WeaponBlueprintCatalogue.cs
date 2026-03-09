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