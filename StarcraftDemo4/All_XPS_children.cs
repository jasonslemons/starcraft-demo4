using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarcraftDemo4
{
    [Serializable]
    sealed class Barracks : ExtendableProducingStructure
    {
        public Barracks(
            List<Structure_Name> _structure_Reqs = null,
            int _minerals_Required = 150,
            int _gas_Required = 0,
            bool _unitQueFull = false,
            bool _upgradeQueFull = false,
            Structure_Name _name = Structure_Name.Barracks,
            int? _production_Time_Left = 60,
            Upgrade _workingUpgrade = null,
            Unit _workingUnit1 = null,
            Unit _workingUnit2 = null,
            Addon _myAddon = null) :
            base(_structure_Reqs, _minerals_Required, _gas_Required, _unitQueFull, _upgradeQueFull, _name,
             _production_Time_Left, _workingUpgrade, _workingUnit1, _workingUnit2, _myAddon)
        {
            structure_Reqs = new List<Structure_Name>();
            structure_Reqs.Add(Structure_Name.Supply_Depot);
        }
    }
    [Serializable]
    sealed class Factory : ExtendableProducingStructure
    {
        public Factory(
            List<Structure_Name> _structure_Reqs = null,
            int _minerals_Required = 150,
            int _gas_Required = 100,
            bool _unitQueFull = false,
            bool _upgradeQueFull = false,
            Structure_Name _name = Structure_Name.Factory,
            int? _production_Time_Left = 60,
            Upgrade _workingUpgrade = null,
            Unit _workingUnit1 = null,
            Unit _workingUnit2 = null,
            Addon _myAddon = null) :
            base(_structure_Reqs, _minerals_Required, _gas_Required, _unitQueFull, _upgradeQueFull, _name,
             _production_Time_Left, _workingUpgrade, _workingUnit1, _workingUnit2, _myAddon)
        {
            structure_Reqs = new List<Structure_Name>();
            structure_Reqs.Add(Structure_Name.Barracks);
        }
    }
    [Serializable]
    sealed class Starport : ExtendableProducingStructure
    {
        public Starport(
            List<Structure_Name> _structure_Reqs = null,
            int _minerals_Required = 150,
            int _gas_Required = 100,
            bool _unitQueFull = false,
            bool _upgradeQueFull = false,
            Structure_Name _name = Structure_Name.Starport,
            int? _production_Time_Left = 50,
            Upgrade _workingUpgrade = null,
            Unit _workingUnit1 = null,
            Unit _workingUnit2 = null,
            Addon _myAddon = null) :
            base(_structure_Reqs, _minerals_Required, _gas_Required, _unitQueFull, _upgradeQueFull, _name,
             _production_Time_Left, _workingUpgrade, _workingUnit1, _workingUnit2, _myAddon)
        {
            structure_Reqs = new List<Structure_Name>();
            structure_Reqs.Add(Structure_Name.Factory);
        }
    }

}
