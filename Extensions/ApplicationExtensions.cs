using System.Text.RegularExpressions;
namespace trackr.Extensions;
public class ApplicationExtensions
{
    public static string[] GetExpArray(string expString)
    {
        expString = expString.Replace("Showing all skills that you have skill in.", String.Empty);
        expString = expString.Replace("SKILL: Rank/Percent towards next rank/Amount learning/Mindstate Fraction", String.Empty);
        expString = Regex.Replace(expString, @"Circle: \d{1,3}", String.Empty);
        expString = Regex.Replace(expString, @"\t|\n|\r", String.Empty);
        expString = Regex.Replace(expString, "\\(.*?\\)", String.Empty);
        expString = Regex.Replace(expString, @"clear|dabbling|perusing|learning|thoughtful|thinking|considering|pondering|ruminating|concentrating|attentive|deliberative|interested|examining|understanding|absorbing|intrigued|scrutinizing|analyzing|studious|focused|very focused|engaged|very engaged|cogitating|fascinated|captivated|engrossed|riveted|very riveted|rapt|very rapt|enthralled|nearly locked|mind lock", String.Empty);
        var expArray = Regex.Split(expString, @" \d\d%");
        expArray = expArray.Select(x => Regex.Replace(x, @"\s+", String.Empty)).ToArray();
        return expArray;
    }

    public static int GetCategoryId(string name)
    {
        var id = 0;
        
        if (CheckSurvivals(name)) 
        {
            id = 4;
        }
        if (CheckMagics(name)) 
        {
            id = 3;
        } 
        if (CheckWeapons(name))
        {
            id = 2;
        }
        if (CheckArmors(name))
        {
            id = 1;
        }
        if (CheckLores(name))
        {
            id = 5;
        }

        return id;
    }

    public static bool CheckSurvivals(string name)
    {
        string[] array = new string[] {
            "Evasion", 
            "Athletics", 
            "Perception", 
            "Stealth", 
            "Locksmithing", 
            "Thievery",
            "FirstAid",
            "Outdoorsmanship",
            "Skinning",
            "Scouting",
            "Backstab",
            "Thanatology"
        };
        
        if (array.Contains(name))
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
    public static bool CheckMagics(string name)
    {
        string[] array = new string[] {
            "LifeMagic", 
            "LunarMagic",
            "ElementalMagic",
            "HolyMagic",
            "ArcaneMagic",
            "InnerMagic",
            "InnerFire",
            "Attunement", 
            "Arcana", 
            "TargetedMagic", 
            "Augmentation", 
            "Debilitation",
            "Utility",
            "Warding",
            "Sorcery",
            "Theurgy",
            "Astrology",
            "Summoning"
        };

        if (array.Contains(name))
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
    public static bool CheckWeapons(string name)
    {
        string[] array = new string[] {
            "ParryAbility", 
            "SmallEdged", 
            "LargeEdged", 
            "TwohandedEdged", 
            "SmallBlunt", 
            "LargeBlunt",
            "TwohandedBlunt",
            "Slings",
            "Bow",
            "Crossbow",
            "Staves",
            "Polearms",
            "LightThrown",
            "HeavyThrown",
            "Brawling",
            "OffhandWeapon",
            "MeleeMastery",
            "MissileMastery",
            "Expertise"
        };

        if (array.Contains(name))
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
    public static bool CheckArmors(string name)
    {
        string[] array = new string[] {
            "ShieldUsage", 
            "LightArmor", 
            "ChainArmor", 
            "Brigandine", 
            "PlateArmor", 
            "Defending",
            "Conviction"
        };

        if (array.Contains(name))
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
    public static bool CheckLores(string name)
    {
        string[] array = new string[] {
            "Empathy", 
            "BardicLore", 
            "Trading",
            "Forging", 
            "Engineering", 
            "Outfitting", 
            "Enchanting",
            "Scholarship",
            "Appraisal",
            "Performance",
            "Tactics",
            "Alchemy",
            "MechanicalLore"
        };

        if (array.Contains(name))
        {
            return true;
        }
        else 
        {
            return false;
        }   
    }
}