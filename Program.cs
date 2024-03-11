using GrawBETA;
using System;
using System.IO.IsolatedStorage;

Console.WriteLine("Gra w BETA");

void Menu()
{
    Console.WriteLine("Witam w grze w beta.");
    Console.WriteLine("1. Graj");
    Console.WriteLine("2. Jak grac?");
    Console.WriteLine("0. Wyjscie z programu");
	string wybierz = Console.ReadLine();
	if (wybierz == "1")
    {
		Gracz x1 = new Gracz();
		Gracz x2 = new Gracz();
		Gracz x3 = new Gracz();
		Gracz x4 = new Gracz();
        List<Gracz> gracze = new List<Gracz>();
        gracze.Add(x1);
        gracze.Add(x2);
        gracze.Add(x3);
        gracze.Add(x4);
		Rozgrywka(gracze);
    }
	else if (wybierz =="0")
	{
		Environment.Exit(0);
	}
	else if (wybierz == "2") 
	{
		Instrukcja();
	}
}

void Instrukcja()
{
	Environment.Exit(0);
}

string Kozer(List<string> Kolory)
{
	Random rnd1 = new Random();

	int kozer = rnd1.Next(0, Kolory.Count);
	string wybranykozer = Kolory[kozer];
    Console.WriteLine("KOZEREM JEST: " + wybranykozer);
    return wybranykozer;
}

void Pojedynek(List<Gracz> gracze, string kozer)
{
	/*
	Kozer k = new Kozer();
    Console.WriteLine("Z KLASY KOZER");
    k.pobierz(kozer);						Wykorzystanie KLASY KOZER (NIEPOTRZEBNE)
	k.pokazKozer();
	Console.WriteLine("Z KLASY KOZER");
	*/
	//Console.WriteLine("ktora karte chcesz rzucic? KOZER : " + kozer);
	string[][] p1 = new string[gracze.Count][];
	for (int i = 0; i < gracze.Count; i++)
	{
		p1[i] = new string[3];
		for (int j = 0; j < 3; j++)
		{
			p1[i][j] = gracze[i].getKarty()[j];
			string str = p1[i][j];
			int firstSpaceIndex = str.IndexOf(' ');
			int secondSpaceIndex = str.IndexOf(' ', firstSpaceIndex + 1);
            string result = str.Substring(firstSpaceIndex + 1, secondSpaceIndex - firstSpaceIndex - 1);
			if (result == kozer)
			{	
				p1[i][j] = p1[i][j] + 0 + 0 + 0 + 0;

				
            }
        }}
	int x;
	List<string> kartygracza = new List<string>();
	List<string> kartybot1 = new List<string>();
	List<string> kartybot2 = new List<string>();
	List<string> kartybot3 = new List<string>();
	int b = 2;
	kartygracza = p1[0].Select(x => x).ToList();
	kartybot1 = p1[1].Select(x => x).ToList();
	kartybot2 = p1[2].Select(x => x).ToList();
	kartybot3 = p1[3].Select(x => x).ToList();
	while (b >= 0)
	{
		x = 0;
		string e = "";
		string f = "";
		string g = "";
		string h = "";
		string znakprzedkozer = "";
		Console.WriteLine("ktora karte rzucasz?");
		gracze[0].aktualizujKarty(kartygracza);
		gracze[0].pokazKarty();
		string input = Console.ReadLine();

		for (int i = 0; i < kartygracza.Count; i++)
		{
			Console.WriteLine("element: " + kartygracza.ElementAt(i));
		}

		if (int.TryParse(input, out x))
		{
			if (x == 1 && !string.IsNullOrEmpty(kartygracza.ElementAt(0)))
			{
				e = kartygracza.ElementAt(0);
				kartygracza[0] = null;
			}
			else if (x == 2 && !string.IsNullOrEmpty(kartygracza.ElementAt(1)))
			{
				e = kartygracza.ElementAt(1);
				kartygracza[1] = null;
			}
			else if (x == 3 && !string.IsNullOrEmpty(kartygracza.ElementAt(2)))
			{
				e = kartygracza.ElementAt(2);
				kartygracza[2] = null;
			}
			else
			{
				Console.WriteLine("Niepoprawny wybór karty lub karta została już rzucona.");
				continue;
			}
		}
		else
		{
			Console.WriteLine("Niepoprawne wejście. Wprowadź liczbę.");
			break;
		}

		if (b >= 0)
		{
			Console.WriteLine("-----------------");
			Console.WriteLine("wybrana karta BOT1: " + kartybot1.ElementAt(b));
			Console.WriteLine("wybrana karta BOT2: " + kartybot2.ElementAt(b));
			Console.WriteLine("wybrana karta BOT3: " + kartybot3.ElementAt(b));
			Console.WriteLine("-----------------");

			if (!string.IsNullOrEmpty(kartybot1.ElementAt(b)))
			{
				f = kartybot1.ElementAt(b);
				kartybot1[b] = null;
			}
			if (!string.IsNullOrEmpty(kartybot2.ElementAt(b)))
			{
				g = kartybot2.ElementAt(b);
				kartybot2[b] = null;
			}
			if (!string.IsNullOrEmpty(kartybot3.ElementAt(b)))
			{
				h = kartybot3.ElementAt(b);
				kartybot3[b] = null;
			}
			

			SprawdzKtoWygral(e, f, g, h, kozer);
		}
		b--;
	}
}


void SprawdzKtoWygral(string e, string f, string g, string h, string kozer)
{
	string KolorKartySchodzacego = "";
    
	int liczba1, liczba2, liczba3, liczba4;
	List<string> listakart = new List<string>();
	listakart.Add(e);
	listakart.Add(f);
	listakart.Add(g);
	listakart.Add(h);
	int firstSpaceIndex = e.IndexOf(' '); // Znajdź pierwszą spację
	int secondSpaceIndex = e.IndexOf(' ', firstSpaceIndex + 1); // Znajdź drugą spację, rozpoczynając od indeksu po pierwszej spacji
	string liczba1str = e.Substring(secondSpaceIndex + 1);
	string kolor1str = e.Substring(firstSpaceIndex + 1, secondSpaceIndex - firstSpaceIndex - 1);

	/*
    Console.WriteLine("#####################################");
    Console.WriteLine(kolor1str);
	Console.WriteLine("#####################################");
	*/
	KolorKartySchodzacego = kolor1str;
	if (int.TryParse(liczba1str, out liczba1)){}
	else{	Console.WriteLine("Nie udało się dokonać konwersji.");}
	
	int firstSpaceIndex1 = f.IndexOf(' '); // Znajdź pierwszą spację
	int secondSpaceIndex1 = f.IndexOf(' ', firstSpaceIndex1 + 1); // Znajdź drugą spację, rozpoczynając od indeksu po pierwszej spacji
	string liczba2str = f.Substring(secondSpaceIndex1 + 1);
	string kolor2str = f.Substring(firstSpaceIndex1 + 1, secondSpaceIndex1 - firstSpaceIndex1 - 1);

	if (int.TryParse(liczba2str, out liczba2)){}
	else{Console.WriteLine("Nie udało się dokonać konwersji.");}
	int firstSpaceIndex2 = g.IndexOf(' '); // Znajdź pierwszą spację
	int secondSpaceIndex2 = g.IndexOf(' ', firstSpaceIndex2 + 1); // Znajdź drugą spację, rozpoczynając od indeksu po pierwszej spacji
	string liczba3str = g.Substring(secondSpaceIndex2 + 1);
	string kolor3str = g.Substring(firstSpaceIndex2 + 1, secondSpaceIndex2 - firstSpaceIndex2 - 1);

	if (int.TryParse(liczba3str, out liczba3)){}
	else{Console.WriteLine("Nie udało się dokonać konwersji.");}

	int firstSpaceIndex3 = h.IndexOf(' '); // Znajdź pierwszą spację
	int secondSpaceIndex3 = h.IndexOf(' ', firstSpaceIndex3 + 1); // Znajdź drugą spację, rozpoczynając od indeksu po pierwszej spacji
	string liczba4str = h.Substring(secondSpaceIndex3 + 1);
	string kolor4str = h.Substring(firstSpaceIndex3 + 1, secondSpaceIndex3 - firstSpaceIndex3 - 1);


	if (int.TryParse(liczba4str, out  liczba4))
	{

		if (kolor1str == KolorKartySchodzacego && kolor1str != kozer) { liczba1 = liczba1 * 100;  listakart[0] = listakart[0] + 0 + 0; }
		if (kolor2str == KolorKartySchodzacego && kolor2str != kozer) { liczba2 = liczba2 * 100;  listakart[1] = listakart[1] + 0 + 0; }
		if (kolor3str == KolorKartySchodzacego && kolor3str != kozer) { liczba3 = liczba3 * 100;  listakart[2] = listakart[2] + 0 + 0; }
		if (kolor4str == KolorKartySchodzacego && kolor4str != kozer) { liczba4 = liczba4 * 100;  listakart[3] = listakart[3] + 0 + 0; }
		int max = liczba1;
		int z = 1; // Zmienna określająca numer gracza z najwyższą kartą
		if (liczba2 > max) { max = liczba2; z = 2; }
		if (liczba3 > max) { max = liczba3; z = 3; }
		if (liczba4 > max) { max = liczba4; z = 4; }
		//Console.WriteLine("WYGRALA KARTA O LICZBIE: " + max);
		//Console.WriteLine(e);
		//Console.WriteLine(listakart[0]);
       // Console.WriteLine(f);
		//Console.WriteLine(g);
		//Console.WriteLine(h);
		for (int i = 0; i < listakart.Count; i++)
		{
			var karta = listakart[i];

			int firstSpaceIndex5 = karta.IndexOf(' '); // Znajdź pierwszą spację
			int secondSpaceIndex5 = karta.IndexOf(' ', firstSpaceIndex5 + 1); // Znajdź drugą spację, rozpoczynając od indeksu po pierwszej spacji
			string liczba5str = karta.Substring(secondSpaceIndex5 + 1);
			string kolor5str = karta.Substring(firstSpaceIndex5 + 1, secondSpaceIndex5 - firstSpaceIndex5 - 1);

			/*
			if (kolor5str == KolorKartySchodzacego)
			{
				karta = karta.PadRight(karta.Length + 2, '0');
				listakart[i] = karta;
			}*/

			int secondSpaceIndexEND = karta.LastIndexOf(' '); // Znajdź indeks ostatniej spacji
			string liczbaKONCOWA = karta.Substring(secondSpaceIndexEND + 1);

			if (int.TryParse(liczbaKONCOWA, out int l))
			{
				if (max == l) // nie zwiekszam wartosci priorytetowej karty schodzacego (bez kozera)
				{
					Console.WriteLine(e + " " + listakart[0]);
					Console.WriteLine(f + " " + listakart[1]);
					Console.WriteLine(g + " " + listakart[2]);
					Console.WriteLine(h + " " + listakart[3]);
					Console.WriteLine("******************************************************");
					Console.WriteLine("Karta która wygrała to : " + karta);
					Console.WriteLine("TURE WYGRYWA GRACZ: " + z);
					Console.WriteLine("******************************************************");
					break;
				}
			}
		}

	}
	else
	{
		Console.WriteLine("Nie udało się dokonać konwersji dla liczba1.");
	}
}



void Rozgrywka(List<Gracz> gracze)
{
    var Znaki = new List<string>()
        {
   //      "Dwa",
		 //"Trzy",
		 //"Cztery",
		 //"Piec",
		 //"Szesc",
		 //"Siedem",
		 //"Osiem",
		 "Dziewiec",
		 "Dziesiec",
		 "Walet",
		 "Dama",
		 "Krol",
		 "As",
		};
    var Kolory = new List<string>()
        {
         "Serce",
         "Zoledz",
         "Wino",
         "Dzwonek",
        };

    string kozerRundy = Kozer(Kolory);

    var Karty = new List<string>();
    int waga = 2;
    int pomocnicza = 0;
    for (int i = 0; i < Znaki.Count; i++)
    {
        for (int j = 0; j < Kolory.Count; j++)
        {
            Karty.Add(Znaki[i] + " " + Kolory[j] + " " + waga);
            pomocnicza++;
        }
        waga++;
		//AS ... 14
		//dwojka ... 2
    }
   // Console.WriteLine("ilosc kart: " + pomocnicza); // INFO O ILOSCI KART
    
    foreach(var karta in Karty)
    {
        Console.WriteLine(karta);  // WYPISANIE WSZYSTKICH KART
    }
    
    foreach (var kartygracza in gracze)
    {
		Losuj(Karty, kartygracza);
	}

	/*
    foreach (var x in gracze)
    {
        x.pokazKarty();
        Console.WriteLine("---------"); POKAZANIE KART WSZYSTKICH GRACZY NARAZ (OTWARTE KARTY)
    }
	*/

    Pojedynek(gracze, kozerRundy);
}

void Losuj(List<string> karty, Gracz gracz)
{
    Random rnd1 = new Random();
    
    int karta1 = rnd1.Next(0, karty.Count);
    string selectedCard1 = karty[karta1];
   // Console.WriteLine(selectedCard1);
    karty.RemoveAt(karta1);

    int karta2 = rnd1.Next(0, karty.Count);
    string selectedCard2 = karty[karta2];
   // Console.WriteLine(selectedCard2);
    karty.RemoveAt(karta2);

	int karta3 = rnd1.Next(0, karty.Count);
	string selectedCard3 = karty[karta3];
	//Console.WriteLine(selectedCard3);
	karty.RemoveAt(karta3);
	gracz.Karty(selectedCard1, selectedCard2, selectedCard3);
	//return (selectedCard1, selectedCard2);
}

Menu();



	
