using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenP3JCevallos.ViewModels;
public class Country
{
    public NameInfo Name { get; set; }
    public string Region { get; set; }
    public MapsInfo Maps { get; set; }

    public string CommonName => Name?.Common ?? "N/A";
    public string MapsLink => Maps?.GoogleMaps ?? "N/A";

    public string RegionWithName => $"{Region} - Josue Cevallos";
}


public class NameInfo
{
    public string Common { get; set; }
}

public class MapsInfo
{
    public string GoogleMaps { get; set; }
}
