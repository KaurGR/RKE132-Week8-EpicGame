/*
string[] heroes = { "SpongeBob", "Shrek", "Minions", "Ninja Turtles" }; 
string[] villains = { "Stinky Pete", "Captain Hook", "Voldemort", "Gru", "Joker" };
string[] weapon = { "stick", "spoon", "banana", "taco chip", "Lego brick" };
Console.WriteLine($"Today {villan} ({villanHP} HP) with {villanWeapon} tries to take over the world!");
Console.WriteLine($"But {hero} ({heroHP} HP) with {heroWeapon} saves the day!");
*/
using System;
using System.IO;

class Program
{
    static void Main()
    {
        string folderPath = @"C:\Users\kaurg\OneDrive\Desktop\ProgeAlgkursus\Week8EpicGame\Data\";
        string heroFile = "heroes.txt";
        string villanFile = "villans.txt";
        string weaponFile = "weapons.txt";

        string[] heroes = File.ReadAllLines(Path.Combine(folderPath, heroFile));
        string[] villans = File.ReadAllLines(Path.Combine(folderPath, villanFile));
        string[] weapons = File.ReadAllLines(Path.Combine(folderPath, weaponFile));

        string villan = GetRandomValueFromArray(villans);
        string villanWeapon = GetRandomValueFromArray(weapons);
        int villanHP = GetCharacterHP(villan);
        int villanStrikeStrength = GetWeaponStrength(villanWeapon);

        string hero = GetRandomValueFromArray(heroes);
        string heroWeapon = GetRandomValueFromArray(weapons);
        int heroHP = GetCharacterHP(hero);
        int heroStrikeStrength = GetWeaponStrength(heroWeapon);


        while (heroHP > 0 && villanHP > 0)
        {
            heroHP -= hit(villan, villanStrikeStrength, hero, heroStrikeStrength, out bool reflectHit);
            Console.WriteLine($"{hero} {heroHP} HP");

            if (!reflectHit)
            {
                villanHP -= hit(hero, heroStrikeStrength, villan, villanStrikeStrength, out _);
                Console.WriteLine($"{villan} {villanHP} HP");
            }

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        if (heroHP > 0)
        {
            Console.WriteLine($"{hero} saves the day!");
        }
        else if (villanHP > 0)
        {
            Console.WriteLine($"{villan} takes over the world!");
        }
        else
        {
            Console.WriteLine("Both fictional characters die!");
        }
    }

    static string GetRandomValueFromArray(string[] someArray)
    {
        Random rnd = new Random();
        int randomIndex = rnd.Next(0, someArray.Length);
        return someArray[randomIndex];
    }

    static int GetCharacterHP(string characterName)
    {
        return characterName.Length < 10 ? 10 : characterName.Length;
    }

    static int hit(string characterName, int weaponStrength, string opponentName, int opponentStrength, out bool reflectHit)
    {
        Random r = new Random();
        int strike = r.Next(1, weaponStrength + 1); // Adjusted for weapon strength
        int criticalChance = r.Next(1, 11); // Random chance for critical hit (1 in 10 chance)
        int reflectChance = r.Next(1, 11); // Random chance to reflect the hit back (1 in 10 chance)

        if (criticalChance == 1) // 1 in 10 chance for a critical hit
        {
            strike *= 2; // Double the damage for a critical hit
            Console.WriteLine($"{characterName} made a critical hit with {strike} damage!");
        }
        else if (reflectChance == 1) // 1 in 10 chance to reflect the hit back
        {
            Console.WriteLine($"{characterName} reflected the hit back to {opponentName}!");
            reflectHit = true; // Set the reflectHit flag to true
            return 0; // Return 0 damage as the hit is reflected
        }
        else
        {
            Console.WriteLine($"{characterName} hit {strike}!");
        }

        reflectHit = false; // Set the reflectHit flag to false
        return strike;
    }

    static int GetWeaponStrength(string weaponName)
    {
        // Add logic to determine weapon strength based on the weapon name
        switch (weaponName)
        {
            case "stick":
                return 2;
            case "spoon":
                return 3;
            case "banana":
                return 4;
            case "taco chip":
                return 5;
            case "Lego brick":
                return 6;
            default:
                return 1; // Default strength
        }
    }
}