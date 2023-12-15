using System.Collections.Generic;
using UnityEngine;

public class WorldModel
{
    [SerializeField] private List<Country> _countries = new List<Country>();
    
    public WorldModel(List<Country> countries)
    {
        _countries = countries;
    }
}