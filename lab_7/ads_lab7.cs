using System;
using static System.Math;
using static System.Console;
using System.Linq;
using System.Collections.Generic;

namespace ads_lab7
{
    public class hashTable
    {
        public Entry[] table;
        public int loadness;
        public int size;
        public string[][] gatesTable;

        private string[] gates = { "1-0xbd7", "2-0xbd3", "1-9iot6", "3-8pol3", "1-4rfd9", "1-1iii1" };
        private Dictionary<string, int> months = new Dictionary<string, int>
        {
            {"january", 31 },
            {"february", 28 },
            {"march", 31 },
            {"april", 30 },
            {"may", 31 },
            {"june", 30 },
            {"july", 31 },
            {"august", 31 },
            {"september", 30 },
            {"october", 31 },
            {"november", 30 },
            {"december", 31 }
        };
        public hashTable(int size1)
        {
            table = new Entry[size1];
            loadness = 0;
            size = size1;
            gatesTable = new string[gates.Length][];
            for(int i = 0; i < gates.Length; i++)
            {
                gatesTable[i] = new string[size];
            }
        }
        public int getHashCode(string str)
        {
            int sum = 0;
            foreach (char ch in str)
            {
                sum += (int)ch;
            }
            return sum % size;
        }         
        public void addGate(string gate, string fCode)
        {
            int index = getHashCode(fCode);
            int pos = Array.IndexOf(gates, gate);
            for(int i = 0; i < size; i++)
            {
                if (gatesTable[pos][index] == null)
                {
                    gatesTable[pos][index] = fCode;
                    return;
                }
                index++;
                index %= size;
            }
        }
        public void deleteGate(string gate, string fCode)
        {
            int pos = Array.IndexOf(gates, gate);
            for(int i = 0; i < size; i++)
            {
                if(gatesTable[pos][i] == fCode)
                {
                    gatesTable[pos][i] = null;
                    return;
                }
            }
        }       
        public void printGates()
        {
            for (int i = 0; i < gates.Length; i++)
            {
                Write(gates[i] + " : ");
                foreach(string fCode in gatesTable[i])
                {
                    if(fCode != null)
                        Write(fCode + " ");
                }
                WriteLine();
            }
        }
        public void reHash()
        {
            size *= 2;
            Array.Resize(ref table, size);
            for (int i = 0; i < gates.Length; i++)
            {
                Array.Resize(ref gatesTable[i], size);
            }
        }
        public void addEntry(Key key, Value value)
        {
            if (loadness * 2 >= size)
            {
                reHash();
            }
            //if entry already exists
            for (int i = 0; i < size; i++)
            {
                if (table[i] == null)
                    continue;
                if (table[i].key.flightCode.Equals(key.flightCode))
                {
                    deleteGate(table[i].value.gate, key.flightCode);
                    addGate(value.gate, key.flightCode);
                    table[i].value = value;
                    return;
                }
            }
            //if doesn't exist, search for empty cell
            int index = getHashCode(key.flightCode);
            for (int i = 0; i < size; i++)
            {
                if (table[index] == null)
                {
                    int gateInd = Array.IndexOf(gates, value.gate);
                    //calculate time difference of latest flight from this gate
                    for (int j = 0; j < gates.Length; j++)
                        {
                        List<Entry> entries = new List<Entry>();
                        foreach (Entry e in table)
                        {
                            if (e == null) continue;
                            if (e.value.gate == value.gate)
                                entries.Add(e);
                        }
                        if (entries.Count == 0)
                        {
                            table[index] = new Entry(key, value);
                            addGate(value.gate, key.flightCode);
                            loadness++;
                            return;
                        }

                        entries.OrderBy(x => x.value.departureTime.year).
                            ThenBy(x => months.Keys.ToList().IndexOf(x.value.departureTime.month)).
                            ThenBy(x => x.value.departureTime.day).ThenBy(x => x.value.departureTime.time);

                        //if difference if big, assign new flight
                        int compared = compareTime(entries.Last().value.departureTime, value.departureTime) - entries.Last().value.isDelayed;
                        if (compared >= 105) 
                        {
                            table[index] = new Entry(key, value);
                            addGate(value.gate, key.flightCode);
                            loadness++;
                            return;
                        }
                        //if difference is low, search for another gate
                        gateInd++;
                        gateInd %= gates.Length;
                        value.gate = gates[gateInd];
                    }
                    //didn't found any gate with appropriate time difference, finding least
                    List<Entry> globalEntries = new List<Entry>();
                    foreach(Entry e in table)
                    {
                        if (e == null) continue;
                        globalEntries.Add(e);
                    }
                    globalEntries.OrderBy(x => x.value.departureTime.year).
                            ThenBy(x => months.Keys.ToList().IndexOf(x.value.departureTime.month)).
                            ThenBy(x => x.value.departureTime.day).ThenBy(x => x.value.departureTime.time);
                    for(int k = globalEntries.Count - 1; k > -1; k--)
                    {
                        if (globalEntries[k] == null) continue;
                        for (int j = k -1; j > -1; j--)
                        {
                            if (globalEntries[j] == null) continue;
                            if (globalEntries[k].value.gate == globalEntries[j].value.gate)
                            {
                                globalEntries[j] = null;
                            }
                        }
                    }
                    globalEntries.RemoveAll(x => x == null);
                    Value val = globalEntries.First().value;
                    value.gate = val.gate;
                    value.isDelayed = val.isDelayed + 105 - compareTime(val.departureTime, value.departureTime);

                    table[index] = new Entry(key, value);
                    addGate(value.gate, key.flightCode);
                    loadness++;
                    return;
                }   
                else
                {
                    index++;
                    index %= size;
                }
            }
        }
        public void removeEntry(Key key)
        {
            for (int i = 0; i < size; i++)
            {
                if (table[i] == null)
                    continue;
                if (table[i].key.flightCode.Equals(key.flightCode))
                {
                    table[i].key.flightCode = "DELETED";
                    deleteGate(table[i].value.gate, key.flightCode);
                    return;
                }
            }
            WriteLine("NOT FOUND");
        }
        public void findEntry(Key key)
        {
            foreach (Entry e in table)
            {
                if (e == null) continue;
                if (e.key.flightCode.Equals(key.flightCode))
                {
                    WriteLine($"{e.key.flightCode} : {e.value.aeroportOfArrival}, {e.value.gate}");
                    return;
                }
            }
            WriteLine("NOT FOUND");
        }
        public void printEntries()
        {
            WriteLine($"Size of Hash Table = {size}");
            foreach (Entry e in table)
            {
                if (e == null)
                    continue;
                WriteLine($"{e.key.flightCode} : {e.value.aeroportOfArrival}, {e.value.gate}\n" +
                    $"Departure time : {e.value.departureTime.timeToString()}, off {e.value.timeToString()}\n");
            }
        }
        public int compareTime(TimeDate old, TimeDate now)
        {
            if (now.year > old.year)
            {
                if (now.month == "january" && old.month == "december")
                {
                    return (31 - old.day + now.day) * 1440 - old.time + now.time;
                }
                return 110;
            }
            if (now.year == old.year)
            {
                int oldm = months.Keys.ToList().IndexOf(old.month);
                int nowm = months.Keys.ToList().IndexOf(now.month);
                if (oldm < nowm)
                {
                    int sum = 0;
                    for (int i = oldm; i < nowm; i++)
                    {
                        sum += months[months.Keys.ToList()[i]];
                    }
                    sum = sum - old.day + now.day;
                    return sum * 1440 - old.time + now.time;
                }
                if (now.month == old.month)
                {
                    if (now.day > old.day)
                    {
                        return (now.day - old.day) * 1440 - old.time + now.time;
                    }
                    if (now.day == old.day)
                    {
                        return now.time - old.time;
                    }
                }
            }
            return -1;
        }

        public class Key
        {
            public string flightCode;
            public Key(string flightCode)
            {
                this.flightCode = flightCode;
            }
        }
        public class Value
        {
            public string aeroportOfArrival;
            public string gate;
            public TimeDate departureTime;
            public int isDelayed;
            private string[] gates = { "1-0xbd7", "2-0xbd3", "1-9iot6", "3-8pol3", "1-4rfd9", "1-1iii1" };
            public Value(string aeroportOfArrival, string gate, TimeDate departureTime, int isDelayed)
            {
                this.aeroportOfArrival = aeroportOfArrival;

                if (gates.Contains(gate)) this.gate = gate;
                else this.gate = gates[3];

                this.departureTime = departureTime;

                this.isDelayed = Min(1439, Max(isDelayed, 0));
            }
            public string timeToString()
            {
                return String.Format("{0:D2}:{1:D2}", isDelayed / 60, isDelayed % 60);
            }
        }
        public class TimeDate
        {
            public int year;
            public string month;
            public int day;
            public int time;
            private Dictionary<string, int> months = new Dictionary<string, int>
        {
            {"january", 31 },
            {"february", 28 },
            {"march", 31 },
            {"april", 30 },
            {"may", 31 },
            {"june", 30 },
            {"july", 31 },
            {"august", 31 },
            {"september", 30 },
            {"october", 31 },
            {"november", 30 },
            {"december", 31 }
        };
            public TimeDate(int year, string month, int day, int time)
            {
                this.year = year;
                if (months.Keys.ToList().Contains(month))
                    this.month = month;
                else
                    this.month = "january";
                this.day = Min(months[month], Max(day, 1));
                this.time = Min(1439, Max(time, 0));
            }
            public string timeToString()
            {
                return String.Format("{0} of {1}, {2}. {3:D2}:{4:D2}", day, month, year, time / 60, time % 60);
            }
        }
        public class Entry
        {
            public Key key;
            public Value value;
            public Entry(Key key, Value value)
            {
                this.key = key;
                this.value = value;
            }
        }

        internal class Program
        {
            static void Main(string[] args)
            {
                var hash = new hashTable(10);
                while (true)
                {
                    WriteLine("[1] Print HashTable\n[2] Add Element\n[3] Delete Element\n[4] Print Element\n[5] Fill With Elements\n[6] Print Gates Table");
                    string input = ReadLine();
                    switch (input)
                    {
                        case "1":
                            hash.printEntries();
                            break;
                        case "2":
                            hash.addEntry(askForKey(), askForValue());
                            break;
                        case "3":
                            hash.removeEntry(askForKey());
                            break;
                        case "4":
                            hash.findEntry(askForKey());
                            break;
                        case "5":
                            hash.addEntry(new Key("asd1"), new Value("Monaco", "1-1iii1", new TimeDate(2021, "april", 3, 1), 0));
                            hash.addEntry(new Key("asd2"), new Value("Kyiv", "1-1iii1", new TimeDate(2021, "april", 3, 2), 0));
                            hash.addEntry(new Key("asd3"), new Value("Moskow", "1-1iii1", new TimeDate(2021, "april", 3, 3), 0));
                            hash.addEntry(new Key("asd4"), new Value("Donbass", "1-1iii1", new TimeDate(2021, "april", 3, 4), 0));
                            hash.addEntry(new Key("asd5"), new Value("Vstavai", "1-1iii1", new TimeDate(2021, "april", 3, 5), 0));
                            hash.addEntry(new Key("asd6"), new Value("Lissabone", "1-1iii1", new TimeDate(2021, "april", 3, 6), 0));
                            hash.addEntry(new Key("asd7"), new Value("Madrid", "1-1iii1", new TimeDate(2021, "april", 3, 7), 0));
                            hash.addEntry(new Key("asd8"), new Value("Milan", "1-1iii1", new TimeDate(2021, "april", 3, 8), 0));
                            hash.addEntry(new Key("asd9"), new Value("Leoben", "1-1iii1", new TimeDate(2021, "april", 3, 120), 0));
                            hash.addEntry(new Key("asd10"), new Value("Leoben-2", "1-1iii1", new TimeDate(2021, "april", 3, 135), 0));
                            hash.addEntry(new Key("asd101"), new Value("Leoben-2", "adfsggggg", new TimeDate(2021, "april", 3, 135), 0));
                            break;
                        case "6":
                            hash.printGates();
                            break;
                    }
                }
            }
            static Key askForKey()
            {
                Write("Input code of flight ");
                string flightCode = ReadLine();
                return new Key(flightCode);
            }
            static Value askForValue()
            {
                Write("\nInput aeroport of arrival ");
                string aeroportOfArrival = ReadLine();
                Write("\nInput gate ");
                string gate = ReadLine();
                TimeDate departureTime = askForTimeDate();
                int isDelayed;
                while (true)
                {
                    Write("\nInput delay time ");
                    string temp = ReadLine();
                    if (int.TryParse(temp, out _))
                    {
                        isDelayed = int.Parse(temp);
                        break;
                    }
                }
                return new Value(aeroportOfArrival, gate, departureTime, isDelayed);
            }
            static TimeDate askForTimeDate()
            {
                int year, day, time; string month;
                while (true)
                {
                    Write("\nInput year ");
                    string temp = ReadLine();
                    if (int.TryParse(temp, out _))
                    {
                        year = int.Parse(temp);
                        break;
                    }
                }
                Write("\nInput month ");
                month = ReadLine();
                while (true)
                {
                    Write("\nInput day ");
                    string temp = ReadLine();
                    if (int.TryParse(temp, out _))
                    {
                        day = int.Parse(temp);
                        break;
                    }
                }
                while (true)
                {
                    Write("\nInput time ");
                    string temp = ReadLine();
                    if (int.TryParse(temp, out _))
                    {
                        time = int.Parse(temp);
                        break;
                    }
                }
                return new TimeDate(year, month, day, time);
            }
        }
    }
}
