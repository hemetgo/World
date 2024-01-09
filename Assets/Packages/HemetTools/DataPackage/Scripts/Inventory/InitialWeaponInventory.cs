
[System.Serializable]
public class InitialWeaponInventory
{
    public ItemSettings InitialMeleeWeapon;
    public FireWeaponSettings InitialPistol;
    public FireWeaponSettings InitialHeavyWeapon;
    public InitialItem InitialItem1;
    public InitialItem InitialItem2;

    public void Initialize()
    {
        WeaponInventoryService.SetMeleeWeapon(InitialMeleeWeapon);
        WeaponInventoryService.SetPistol(InitialPistol);
        WeaponInventoryService.SetHeavyWeapon(InitialHeavyWeapon);
        if (InitialItem1 != null) WeaponInventoryService.AddItem(InitialItem1.Item, InitialItem1.Amount);
        if (InitialItem1 != null) WeaponInventoryService.AddItem(InitialItem2.Item, InitialItem2.Amount);
    }
}

[System.Serializable]
public class InitialItem
{
    public ItemSettings Item;
    public int Amount;
}
