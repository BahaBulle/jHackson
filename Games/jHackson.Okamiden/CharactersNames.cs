﻿// <copyright file="CharactersNames.cs" company="BahaBulle">
//     Copyright (c) BahaBulle. All rights reserved.
// </copyright>

namespace jHackson.Okamiden
{
    using System.Collections.Generic;

    internal static class CharactersNames
    {
        public static Dictionary<int, string> Characters { get; } = new Dictionary<int, string>
        {
            [0x0000] = "???",
            [0x0001] = "Issun",
            [0x0002] = "Sakuya",
            [0x0003] = "Young Girl",
            [0x0004] = "Susano",
            [0x0005] = "Kushi",
            [0x0006] = "Mr. Orange",
            [0x0007] = "Mrs. Orange",
            [0x0008] = "Mushi's Mama",
            [0x0009] = "Mushi",
            [0x000A] = "Hayabusa",
            [0x000B] = "Nameless Man",
            [0x000C] = "Kuni",
            [0x000D] = "Old Man",
            [0x000E] = "Old Woman",
            [0x000F] = "Man",
            [0x0010] = "Woman",
            [0x0011] = "Merchant",
            [0x0012] = "Ayame",
            [0x0013] = "Dr. Redbeard",
            [0x0014] = "Yomigami",
            [0x0015] = "Michigami",
            [0x0016] = "Tachigami",
            [0x0017] = "Sakigami",
            [0x0018] = "Master Anura",
            [0x0019] = "Merchant",
            [0x001A] = "Ayame's Mother",
            [0x001B] = "Tama",
            [0x001C] = "Kokari",
            [0x001D] = "Ume",
            [0x001E] = "Madame Fawn",
            [0x001F] = "Karude",
            [0x0020] = "MC",
            [0x0021] = "Witch Queen",
            [0x0022] = "Performer",
            [0x0023] = "Stagehand",
            [0x0024] = "Princess Fuse",
            [0x0025] = "Chi",
            [0x0026] = "Shin",
            [0x0027] = "Rei",
            [0x0028] = "Tei",
            [0x0029] = "Ko",
            [0x002A] = "Tsuruya",
            [0x002B] = "Kiku",
            [0x002C] = "Soldier",
            [0x002D] = "Soldier",
            [0x002E] = "Raiden",
            [0x002F] = "Mr. Chic",
            [0x0030] = "Yama",
            [0x0031] = "Mr. Flower",
            [0x0032] = "Momotaro",
            [0x0033] = "Samurai Dandy",
            [0x0034] = "Guardsman",
            [0x0035] = "Guardsman",
            [0x0036] = "Kagu",
            [0x0037] = "Owner",
            [0x0038] = "Stagehand",
            [0x0039] = "Stagehand",
            [0x003A] = "Stagehand",
            [0x003B] = "Robed Man",
            [0x003C] = "Robed Man",
            [0x003D] = "Robed Man",
            [0x003E] = "Robed Man",
            [0x003F] = "Miko Cho",
            [0x0040] = "Stage Spirit",
            [0x0041] = "Toilet Spirit",
            [0x0042] = "Prop Spirit",
            [0x0043] = "Costume Spirit",
            [0x0044] = "Actress",
            [0x0045] = "City Dweller",
            [0x0046] = "City Dweller",
            [0x0047] = "City Dweller",
            [0x0048] = "Girl",
            [0x0049] = "Naguri",
            [0x004A] = "Girl",
            [0x004B] = "Hightower",
            [0x004C] = "Fairy Guard",
            [0x004D] = "Fairy Guard",
            [0x004E] = "Fairy Guard",
            [0x004F] = "Merchant",
            [0x0050] = "Merchant",
            [0x0051] = "Merchant",
            [0x0052] = "Merchant",
            [0x0053] = "Informant",
            [0x0054] = "Merchant",
            [0x0055] = "Guard",
            [0x0056] = "Guard",
            [0x0057] = "Fairy",
            [0x0058] = "Fairy",
            [0x0059] = "Green Imp?",
            [0x005A] = "Nanami",
            [0x005B] = "Fisherman",
            [0x005C] = "Fisherman",
            [0x005D] = "Father",
            [0x005E] = "Man",
            [0x005F] = "Sailor",
            [0x0060] = "Scholar",
            [0x0061] = "Kurow",
            [0x0062] = "King Fury",
            [0x0063] = "Bluebeard",
            [0x0064] = "Commoner Boy",
            [0x0065] = "Nuregami",
            [0x0066] = "Bakugami",
            [0x0067] = "Tsutagami",
            [0x0068] = "Moegami",
            [0x0069] = "Kazegami",
            [0x006A] = "Gekigami",
            [0x006B] = "Kyokugami",
            [0x006C] = "Mika",
            [0x006D] = "Pupil",
            [0x006E] = "Priest",
            [0x006F] = "Man",
            [0x0070] = "Play-crazed Girl",
            [0x0071] = "Thunder Boy",
            [0x0072] = "Thunder Man",
            [0x0073] = "Thunder Boy",
            [0x0074] = "Thunder Lady",
            [0x0075] = "Thunder Girl",
            [0x0076] = "Drummer",
            [0x0077] = "Keyboardist",
            [0x0078] = "Maraquero",
            [0x0079] = "Guitarist",
            [0x007A] = "Shakuya",
            [0x007B] = "Mr. Fruit",
            [0x007C] = "Mrs. Fruit",
            [0x007D] = "Nazo's Mama",
            [0x007E] = "Nami",
            [0x007F] = "Nagi",
            [0x0080] = "Nazo",
            [0x0081] = "Nazo's Dog",
            [0x0082] = "Play-crazed Mom",
            [0x0083] = "Man",
            [0x0084] = "Forest Man",
            [0x0085] = "Forest Woman",
            [0x0086] = "Manpuku",
            [0x0087] = "Bullhead",
            [0x0088] = "Sen",
            [0x0089] = "Ryo",
            [0x008A] = "Sen & Ryo",
            [0x008B] = "Lil' Devil",
            [0x008C] = "Gen",
            [0x008D] = "Aristocrat",
            [0x008E] = "Aristocrat",
            [0x008F] = "Aristocrat",
            [0x0090] = "Aristocrat",
            [0x0091] = "Soldier",
            [0x0092] = "Guardsman",
            [0x0093] = "Guardsman",
            [0x0094] = "Guardsman",
            [0x0095] = "Investigator",
            [0x0096] = "Investigator",
            [0x0097] = "Investigator",
            [0x0098] = "Stagehand",
            [0x0099] = "Stagehand",
            [0x009A] = "Stagehand",
            [0x009B] = "Actor",
            [0x009C] = "Actor",
            [0x009D] = "Actress",
            [0x009E] = "Spectator",
            [0x009F] = "Spectator",
            [0x00A0] = "Spectator",
            [0x00A1] = "Red Imp",
            [0x00A2] = "Kage",
            [0x00A3] = "Sugawara",
            [0x00A4] = "Akuro",
            [0x00A5] = "Old Sailor",
            [0x00A6] = "Benkei",
            [0x00A7] = "Captain",
            [0x00A8] = "Otohime",
            [0x00A9] = "Shikibu",
            [0x00AA] = "Genji",
            [0x00AB] = "Aristocrat",
            [0x00AC] = "Aristocrat",
            [0x00AD] = "Aristocrat",
            [0x00AE] = "Umami",
            [0x00AF] = "Aji",
            [0x00B0] = "Isshaku",
            [0x00B1] = "Charity",
            [0x00B2] = "Yokai",
            [0x00B3] = "Merchant",
            [0x00B4] = "Fisherman",
            [0x00B5] = "Fisherman",
            [0x00B6] = "",
            [0x00B7] = "Merchant",
            [0x00B8] = "Merchant",
            [0x00B9] = "Merchant",
            [0x00BA] = "Merchant",
            [0x00BB] = "Merchant",
            [0x00BC] = "Merchant",
            [0x00BD] = "Merchant",
            [0x00BE] = "Merchant",
            [0x00BF] = "Imp",
            [0x00C0] = "Imp",
            [0x00C1] = "Imp",
            [0x00C2] = "Imp",
            [0x00C3] = "Imp",
            [0x00C4] = "Imp",
            [0x00C5] = "Imp",
            [0x00C6] = "Imp",
            [0x00C7] = "Imp",
            [0x00C8] = "Imp",
            [0x00C9] = "Prisoner",
            [0x00CA] = "Prisoner",
            [0x00CB] = "Prisoner",
            [0x00CC] = "Prisoner",
            [0x00CD] = "Merchant",
            [0x00CE] = "Imp",
            [0x00CF] = "Imp",
            [0x00D0] = "Merchant",
            [0x00D1] = "Merchant",
            [0x00D2] = "Merchant",
            [0x00D3] = "Merchant",
            [0x00D4] = "Scholar",
            [0x00D5] = "Merchant",
            [0x00D6] = "Merchant",
            [0x00D7] = "Merchant",
            [0x00D8] = "Guard",
            [0x00D9] = "Guard",
            [0x00DA] = "Imp",
            [0x00DB] = "Lost Girl",
            [0x00DC] = "Yaku",
            [0x00DD] = "Old Woman",
            [0x00DE] = "Old Man",
            [0x00DF] = "Boar",
            [0x00E0] = "Aristocrat",
            [0x00E1] = "Tanuki",
            [0x00E2] = "Shady Merchant",
            [0x00E3] = "Grimm",
            [0x00E4] = "Old Woman",
            [0x00E5] = "Boy",
            [0x00E6] = "Thunder",
            [0x00E7] = "Sake Brewer",
            [0x00E8] = "Pheasant",
            [0x00E9] = "Dog",
            [0x00EA] = "Puppy",
            [0x00EB] = "Crane",
            [0x00EC] = "Girl",
            [0x00ED] = "Monkey",
            [0x00EE] = "Retired Fisher",
            [0x00EF] = "Merchant",
            [0x00F0] = "Blacksmith",
            [0x00F1] = "Shaman",
            [0x00F2] = "Nanami",
            [0x00F3] = "Skull",
            [0x00F4] = "Kurow",
            [0x00F5] = "Chibiterasu",
            [0x00F6] = "Kurow",
            [0x00F7] = "Akuro",
            [0x00F8] = "???",
            [0x00F9] = "Shiranui",
            [0x00FA] = "Witch Queen",
            [0x00FB] = "Nameless Man",
            [0x00FC] = "Susano",
            [0x00FD] = "Merchant",
            [0x00FE] = "Nagi",
            [0x00FF] = "Jin",
            [0x0100] = "Jin",
            [0x0101] = "Jin",
            [0x0102] = "Jin",
            [0x0103] = "",
            [0x0104] = "",
            [0x0105] = "",
            [0x0106] = "",
            [0x0107] = "",
            [0x0108] = "Sailor",
            [0x0109] = "Kuni",
            [0x010A] = "Kurow",
            [0x010B] = "Otohime",
            [0x010C] = "サル",
            [0x010D] = "サル",
            [0x010E] = "サル",
            [0x010F] = "スズメ",
            [0x0110] = "スズメ",
            [0x0111] = "スズメ",
            [0x0112] = "ウサギ",
            [0x0113] = "ウサギ",
            [0x0114] = "ウサギ",
            [0x0115] = "うりぼう",
            [0x0116] = "うりぼう2",
            [0x0117] = "うりぼう3",
            [0x0118] = "キジ",
            [0x0119] = "キジ",
            [0x011A] = "キジ",
            [0x011B] = "イヌ",
            [0x011C] = "イヌ",
            [0x011D] = "イヌ",
            [0x011E] = "タヌキ",
            [0x011F] = "タヌキ",
            [0x0120] = "タヌキ",
        };
    }
}