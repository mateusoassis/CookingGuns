using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _WeaponNaming : MonoBehaviour
{
    [Header("Cores/qualidade de nomes de armas")]
    public Color uncommonColor;
    public Color rareColor;
    public Color epicColor;
    public Color uniqueColor;
    public Color legendaryColor;

    [Header("Pistolas")]
    public string[] pistolPrefixoDe;
    public string[] pistolSufixoDe;
    public string[] pistolPrefixo;
    public string[] pistolSufixo;
    public string[] pistolUniqueName;

    [Header("Shotguns")]
    public string[] shotgunPrefixoDe;
    public string[] shotgunSufixoDe;
    public string[] shotgunPrefixo;
    public string[] shotgunSufixo;
    public string[] shotgunUniqueName;

    [Header("Machine Guns")]
    public string[] machineGunPrefixoDe;
    public string[] machineGunSufixoDe;
    public string[] machineGunPrefixo;
    public string[] machineGunSufixo;
    public string[] machineGunUniqueName;

    [Header("Grenade Launchers")]
    public string[] grenadeLauncherPrefixoDe;
    public string[] grenadeLauncherSufixoDe;
    public string[] grenadeLauncherPrefixo;
    public string[] grenadeLauncherSufixo;
    public string[] grenadeLauncherUniqueName;
}
