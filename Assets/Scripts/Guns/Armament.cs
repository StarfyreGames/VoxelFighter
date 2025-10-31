using System.Collections.Generic;
using System.Net.NetworkInformation;
using Guns.Clip;
using Guns.Modifications;
using UnityEngine;

namespace Guns
{
    public class Armament
    {
        private readonly Specs _masterSpecsTemplate;
        private readonly GameObject _host;
        private readonly List<Vector3> _offsets;
        private readonly List<Gun> _guns = new();

        public Armament(List<Vector3> offsets, Vector3 rotation, BulletSpec.Origin origin, BulletCatalogue catalogue,
            GameObject host)
        {
            _host = host;
            _offsets = offsets;
            _masterSpecsTemplate = new Specs(host, rotation, origin, catalogue);
        }

        public void AddGun()
        {
            if (_offsets.Count <= _guns.Count) return;

            var offset = _offsets[_guns.Count];
            var gun = new Gun(_masterSpecsTemplate.CloneForGun(offset));
            _guns.Add(gun);
        }

        public void Update(float deltaTime)
        {
            _guns.ForEach(gun => gun.Update(deltaTime));
        }

        public void Fire()
        {
            _guns.ForEach(gun => gun.Fire());
        }

        public void DrawGizmos()
        {
            _guns.ForEach(gun => gun.DrawGizmo());
        }

        public void ApplyModification(AModification modification)
        {
            _guns.ForEach(gun => gun.ApplyModification(modification));
            _masterSpecsTemplate.ApplyModification(modification);
            modification.Modify(this);
        }
    }
}