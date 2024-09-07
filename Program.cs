using GrawBETA;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;
// NAPRAWIC - w przypadku czterech graczy i gdy wszysczy czterej wymienia  wszystie trzy karty - nie moge wyrzucac kozera z rozdania
// zrobic ifa ze jezeli lista jest pusta zeby brala kozera
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
    Console.WriteLine("-------------------------------------------------------------- INSTRUKCJA GRY --------------------------------------------------------------");
    Console.WriteLine("Ilosc graczy = 3 lub 4");
    Console.WriteLine("Kazdy z graczy otrzymuje po trzy karty. Zanim Rozpocznie sie tura:");
    Console.WriteLine("Przy prawdziwym stole (nie w programie) gracz rozdający wybierany jest przez wyciągniecie jak najwyższej karty (losowo z talii kart)");
    Console.WriteLine("Zawsze pierwsza tura przy stole jest tura przymusowa (jest przymus grania - dotyczy to gry o jakas stawke)");
    Console.WriteLine("Jezeli gra jest o stawke to stawka powinna latwo dzielic sie przez 3 (niezaleznie czy gra trzech czy czterech graczy) na przyklad 4.50");
    Console.WriteLine("4.50 to jest przypadek gdy gra trzech graczy i kazdy wchodzi po 1.50 w przypadku czterech graczy kwota bedzie sie roznila");
    Console.WriteLine("Kozerem nazywa sie karte dominujaca dane rozdanie. Jest ona wybierania po rozdaniu kazdemu z graczy 3 kart");
    Console.WriteLine("Rozdajacy po zobaczeniu jaka karta jest kozer przekladza ta karte na spod talii LUB kupuje*");
    Console.WriteLine("Karty rozdaje sie po jednej zaczynając od gracza po lewej i idąc dalej w tamtym kierunku");
    Console.WriteLine("GRA JEST NA 24 karty - od 9 do As");
	Console.WriteLine("Osoba której rozdający jako pierwszej rozdał kartę ma pierwszeństwo w decydowaniu (pierwsza osoba po lewej)");
    Console.WriteLine("o tym, czy chce zagrac w danej partii (patrzy jakie ma karty i decyduje)");
    Console.WriteLine("Nie mozna (lepiej tego nie robic) ''wychylac sie'' - czyli mowic ze sie gra przed osoba ktora wczesniej miala to oznajmic w kolejce");
    Console.WriteLine("*Tylko osoba rozdajaca moze kupic karte. Wtedy musi odrzucic jedna z trzech ktore sobie rozdala i NIE MA MOZLIWOSCI wymiany kart");
	Console.WriteLine("############################################################################################################################################");

    Console.WriteLine("Osoba ktora rozdaje wplaca zawsze najnizsza pojedyncza kwote z pierwszego rozdania - na przyklad 1.50 (w pierwszym rozdaniu kazdy po 1.50)");

    Console.WriteLine("Schodzenie i LEWE");
    Console.WriteLine("Pierwsza osoba ktora ''Schodzi'' - czyli wyrzuca swoja karte na srodek stolu - jest to pierwsza osoba na lewo od rozdajacego");
    Console.WriteLine("Jezeli osoba schodzi nie z karty kozera to trzeba zejsc pod ta karte, czyli:");
	Console.WriteLine("KOZER : ZOLEDZ ; pierwsza karta rzucona to serce - Jezeli masz w swoich kartach serce i kozer to musisz rzucic do serca");
    Console.WriteLine("Jezeli nie mialbys serca to rzucasz kozer. Jezeli nie masz ani serca ani kozera to rzucasz dowolna inna karte");
    Console.WriteLine("W trakcie rozgrywki sa trzy pojedynki gdzie kazdy rzuca po karcie: najmocniejsza karta w pojedynku wygrywa (nazywa sie to wygrana lewa)");
    Console.WriteLine("Zgarniecie jednej LEWEJ z trzech (patrz linijke wyzej) powoduje wygranie 1/3 calej puli na stole");
	Console.WriteLine("Zgarniecie dwoch LEWYCH z trzech (patrz wyzej) powoduje wygranie 2/3 calej puli na stole");
	Console.WriteLine("Zgarniecie trzech LEWYCH z trzech (patrz wyzej) powoduje wygranie calej puli na stole");
	Console.WriteLine("Nie zgarniecie ZADNEJ LEWEJ ze stolu powoduje ze gracz ''sie zbecil'' - czyli musi ");
    Console.WriteLine("na stol polozyc ze swoich pieniedzy cala pule ktora sie na tym stole znajdowala");
    Console.WriteLine("Jezeli gracz kupil karte (ktora okresla kozera) to MUSI wygrac w rozdaniu conajmniej DWIE LEWE");
    Console.WriteLine("Jezeli nie wygra DWOCH LEWYCH - oznacza to ze ''zbecil sie'' PODWOJNIE - musi zaplacic to co bylo na stole X2");
    Console.WriteLine("Jezeli po kupieniu karty gracz wygra dwie lub wiecej lewych to po prostu wygrywa i ktos inny ''sie zbecil''");

    Console.WriteLine("Po skonczeniu jednej partii rozdajacy daje karty do nowego rozdajacego (pierwszej osoby po lewej)");
    Console.WriteLine("Nowy rozdajacy musi wplacic podstawowa kwote rozdania (pisalem juz o tym wyzej)");
    Console.WriteLine("W przypadku gdy np. jeden gracz zrezygnuje i zostanie dwoch w grze oraz dojdzie do sytuacji gdzie jedna osoba:");
    Console.WriteLine("Zgarnie dwie lewe, a druga osoba jedna - nastepuje podzial pieniedzmi i powrot do pierwszej podstawowej puli (np. 4.50");

    Console.WriteLine("PS. Radze uwazac - mozna wrocic bez spodni do domu :)");
    Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------");

	Menu();

}

string Kozer(List<string> Karty)
{
	Random rnd1 = new Random();

	int kozer = rnd1.Next(0, Karty.Count);
	string wybranykozer = Karty[kozer];
    Console.WriteLine("KOZEREM JEST: " + wybranykozer);
    return wybranykozer;
}
string KolorKozer(string karta)
{
	int firstSpaceIndex = karta.IndexOf(' '); // Znajdź pierwszą spację
	int secondSpaceIndex = karta.IndexOf(' ', firstSpaceIndex + 1); // Znajdź drugą spację, rozpoczynając od indeksu po pierwszej spacji
	string liczba1str = karta.Substring(secondSpaceIndex + 1);
	string kolor1str = karta.Substring(firstSpaceIndex + 1, secondSpaceIndex - firstSpaceIndex - 1);
	return kolor1str;
}

void Ktoijakakartewyrzuca(List<Gracz> gracze, string kozer, int k, string[][] p1)
{
	List<string> KartyRzucone = new List<string>();
	// CzyRzucona() niepotrzebne bo dam jako 3 argument w WybierzKarteBOT i WybierzKarteGracz liste wyżej.
	// to spowoduje że jak bedzie lista pusta to bedzie napis "Rzucasz pierwszy!" a jak nie to będzie lista
	// rzuconych wcześniej kart
	Console.WriteLine("Rozdanie zaczyna gracz: " + (k+1));
	//Console.WriteLine(p1[0][1]);
	int pom = 0;
	int pom1 = 0;
	while (pom < 3)
	{
		while (pom1 < 4)
		{
			
				if (k != 0)
				{
					string x = WybierzKarteBOT(k, p1, KartyRzucone); //tu zaczyna bot
					if (k == 3) // tu bede musial zmienic jezeli bede chcial zagrac w 4 osoby (poki co jest na 3 osoby)
					{
						k = 0;
					}
					pom++;
					k++;
				pom1++;
				}
				else if (k == 0)
				{
					WybierzKarteGracz(k, p1, KartyRzucone);
					// tutaj całe to co napisałem z wybieraniem karty przez gracza
					pom++;
					k++;
				pom1++;
				}
			
		}
		
		
		
		
	}
	
}

string WybierzKarteBOT(int k, string[][] p1, List<string>KartyRzucone)
{
	string wybranakarta = "";
	for (int i = 0; i < 3; i++)
	{
		Console.WriteLine(p1[k][i]);
	}

	// trzeba to zrobic tak:

	// bot przegląda talię i wybiera karte która będzie najstosowniejsza.
	// jeżeli ma Asa kozernego to zawsze z niego schodzi
	// jeżeli ma króla kozernego to też zawsze z niego schodzi
	// jak ma niskiego kozera to schodzi z innej karty która jest wysoka (wyższa niż ta trzecia)
	// jak ma dwa kozery to schodzi z obcej
	// jak trzy kozery to z najniższego kozera chyba ze ma asa lub króla

	int max = 0;
	for (int i = 0; i < p1[k].Length; i++)
	{
		string str = p1[k][i]; // Pobieramy opis karty gracza

		// Znalezienie trzeciej spacji, po której jest liczba
		int firstSpaceIndex = str.IndexOf(' ');
		int secondSpaceIndex = str.IndexOf(' ', firstSpaceIndex + 1);
		string liczbaString = str.Substring(secondSpaceIndex + 1);

		int liczba = int.Parse(liczbaString); // Zamiana na liczbę
		if(liczba == 120000 || liczba == 110000)
		{
			wybranakarta = p1[k][i]; break;
		}
		wybranakarta = p1[k][i];
	}
	return wybranakarta;

}

void WybierzKarteGracz(int k, string[][] p1, List<string> KartyRzucone)
{



}

void Pojedynek(List<Gracz> gracze, string kozer)
{
	int k = 5 ; // k = 5 na poczatku rozdania zeby bylo wiadomo żeby dać random kto zaczyna
	/*
	Kozer k = new Kozer();
    Console.WriteLine("Z KLASY KOZER");
    k.pobierz(kozer);						Wykorzystanie KLASY KOZER (NIEPOTRZEBNE)
	k.pokazKozer();
	Console.WriteLine("Z KLASY KOZER");
	
	//Console.WriteLine("ktora karte chcesz rzucic? KOZER : " + kozer);
	*/
	if (k == 5)
	{
		Random rndm = new Random();
		int liczba = rndm.Next(0, 4);
		//Console.WriteLine("gracz ktory zaczyna to: " + liczba);
		k = liczba;
	}




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


	Ktoijakakartewyrzuca(gracze, kozer, k, p1);
	


	// dodac Random w celu wybrania gracza który pierwszy wyrzuca karte

	while (b >= 0)
	{
		x = 0;
		string e = "";
		string f = "";
		string g = "";
		string h = "";
		string znakprzedkozer = "";
		Console.WriteLine("ktora karte rzucasz? (wpisz 1, 2 lub 3)");
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
			continue;
			
		}
		Console.WriteLine("e = " + e);
        Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
        gracze[1].pokazKarty();
		gracze[2].pokazKarty();
		gracze[3].pokazKarty();
		Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$");

		// tu powinno być sprawdzenie jaką kartę  (a dokladnie figure karty) wyrzuca gracz bo
		// BOT musi rzucić albo kolor pasujący do
		// karty wyrzuconej przez gracza albo kozera

		// latwo to dodac - wystarczy wstawic if(karta > 100) potem kolejnego ifa (karta > 10000)
		// potem sprawdzić czy karta wygra z kartą kozerową na stole jezeli powyzej 10000 i wybrac najslabszą
		//

		/*
		 * funkcja SprawdzKarty()
		 * jezeli wartosc znaku ktora rzuca bot != wartosci znaku schodzącego
		 * wtedy sprawdz czy to co wyrzuca bot == kozer
		 * jezeli tak to sprawdz czy nie jest to jedyna taka karta (jezeli ma dwa kozery to moze miec wybor)
		 * 
		 * wyrzuc kozer
		 * jezeli nie to szukaj karty ze znakiem karty schodzacego
		 * jezeli taka jest to znowu sprawdzic czy nie ma takiej wiecej niz jedna 
		 * jezeli jest wiecej niz jedna to dać wybór (po prostu zrobić losowość póki co może)
		 * 
		 * 
		 * */


		// i jezeli nie ma ani takiej ani takiej to wtedy wyrzuca dowolna
		int firstSpaceIndexL = e.IndexOf(' '); // Znajdź pierwszą spację
		int secondSpaceIndexL = e.IndexOf(' ', firstSpaceIndexL + 1); // Znajdź drugą spację, rozpoczynając od indeksu po pierwszej spacji
		string liczba1strL = e.Substring(secondSpaceIndexL + 1);

		if (b >= 0)
		{
			//WybierzOdpowiednieKarty(kartybot1, kartybot2, kartybot3, kozer, e);
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
			
			k = SprawdzKtoWygral(e, f, g, h, kozer, gracze, b);

			Console.WriteLine("RETURN Z FUNKCJI SPRAWDZKTOWYGRAL -------------------------------" + k);
        }
		b--;
	}
	Console.WriteLine("kontynuowac? (1. TAK) (2. NIE)");
	string x1 = Console.ReadLine();

	if (x1 == "1")
	{
		Rozgrywka(gracze);
	}
	else
	{
		Environment.Exit(0);
	}
}



List<int> WybierzOdpowiednieKarty(List<string> kartyBOT1,
								  List<string> kartyBOT2,
								  List<string> kartyBOT3,
							      string kozer, string kartagracza)
{
	List<int> WybranaKarta = new List<int>();
	int x = 0;
	int y = 0;
	int z = 0;

	int firstSpaceIndexgracza = kartagracza.IndexOf(' '); // Znajdź pierwszą spację
	int secondSpaceIndexgracza = kartagracza.IndexOf(' ', firstSpaceIndexgracza + 1); // Znajdź drugą spację, rozpoczynając od indeksu po pierwszej spacji
	string liczbagracza = kartagracza.Substring(secondSpaceIndexgracza + 1);
	string kolorgracza = kartagracza.Substring(firstSpaceIndexgracza + 1, secondSpaceIndexgracza - firstSpaceIndexgracza - 1);

	// Najpierw sprawdz czy masz do koloru schodzacego
	foreach (string s in kartyBOT1)
	{
		int firstSpaceIndex1 = s.IndexOf(' '); // Znajdź pierwszą spację
		int secondSpaceIndex1 = s.IndexOf(' ', firstSpaceIndex1 + 1); // Znajdź drugą spację, rozpoczynając od indeksu po pierwszej spacji
		string liczba2str = s.Substring(secondSpaceIndex1 + 1);
		string kolor2str = s.Substring(firstSpaceIndex1 + 1, secondSpaceIndex1 - firstSpaceIndex1 - 1);

		Console.WriteLine(s + "                  ");
	}
	// jezeli masz to potem sprawdz czy masz lepsza karte koloru schodzacego niz schodzacy

	// jezeli nie masz wgl karty koloru schodzacego to sprawdz czy masz kozer

	// jezeli masz kozer to rzucaj najslabszy kozer

	// jezeli nie masz to rzucaj po prostu kozer

	// jezeli nie masz ani kozera ani koloru karty schodzacego to rzucaj dowolna

	return WybranaKarta;
}

int SprawdzKtoWygral(string e, string f, string g, string h, string kozer, List<Gracz> gracze, int tura)
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
	else{Console.WriteLine("Nie udało się dokonać konwersji.");}

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
	int z = 1; // Zmienna określająca numer gracza z najwyższą kartą


	if (int.TryParse(liczba4str, out  liczba4))
	{

		if (kolor1str == KolorKartySchodzacego && kolor1str != kozer) { liczba1 = liczba1 * 100;  listakart[0] = listakart[0] + 0 + 0; }
		if (kolor2str == KolorKartySchodzacego && kolor2str != kozer) { liczba2 = liczba2 * 100;  listakart[1] = listakart[1] + 0 + 0; }
		if (kolor3str == KolorKartySchodzacego && kolor3str != kozer) { liczba3 = liczba3 * 100;  listakart[2] = listakart[2] + 0 + 0; }
		if (kolor4str == KolorKartySchodzacego && kolor4str != kozer) { liczba4 = liczba4 * 100;  listakart[3] = listakart[3] + 0 + 0; }
		int max = liczba1;
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

					switch (z)
					{
						case 1: { gracze[0].LewawRundzie(); break; }
						case 2: { gracze[1].LewawRundzie(); break; }
						case 3: { gracze[2].LewawRundzie(); break; }
						case 4: { gracze[3].LewawRundzie(); break; }
					}
					gracze[0].pokazLewe();
					if(tura == 0)
					{
						int a1, a2, a3, a4;
						a1 = gracze[0].getLewe();
						a2 = gracze[1].getLewe();
						a3 = gracze[2].getLewe();
						a4 = gracze[3].getLewe();

						Console.WriteLine("######################################################");
                        Console.WriteLine("ILOSC WYGRANYCH TUR: ");
						Console.WriteLine("GRACZ1");
						gracze[0].pokazLewe();
						if(a1 == 0)
						{
                            Console.WriteLine("----------------------");
                            Console.WriteLine("GRACZ1 ZOSTAŁ ZBECONY");
							Console.WriteLine("----------------------");

						}
						Console.WriteLine("GRACZ2");
						gracze[1].pokazLewe();
						if (a2 == 0)
						{
							Console.WriteLine("----------------------");
							Console.WriteLine("GRACZ2 ZOSTAŁ ZBECONY");
							Console.WriteLine("----------------------");

						}
						Console.WriteLine("GRACZ3");
						gracze[2].pokazLewe();
						if (a3 == 0)
						{
							Console.WriteLine("----------------------");
							Console.WriteLine("GRACZ3 ZOSTAŁ ZBECONY");
							Console.WriteLine("----------------------");

						}
						Console.WriteLine("GRACZ4");
						gracze[3].pokazLewe();
						if (a4 == 0)
						{
							Console.WriteLine("----------------------");
							Console.WriteLine("GRACZ4 ZOSTAŁ ZBECONY");
							Console.WriteLine("----------------------");
						}
						Console.WriteLine("######################################################");
						for (int c = 0; c < gracze.Count; c++){gracze[c].zerujLewe();}
					}
					break;
				}
			}
		}

	}
	else
	{
		Console.WriteLine("Nie udało się dokonać konwersji dla liczba1.");
	}
	return z - 1;
}

void Rozgrywka(List<Gracz> gracze)
{
    var Znaki = new List<string>()
        {
		 //"Dwa",
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
	string kozerRundy = Kozer(Karty);
	Karty.Remove(kozerRundy);
	string kozerKolor = KolorKozer(kozerRundy);// JEZELI KUPUJE TO NIE REMOVE TO TRZEBA BEDZIE JAKOS ZMIENIC!!!!!!!!!
	// Console.WriteLine("ilosc kart: " + pomocnicza); // INFO O ILOSCI KART

	/*
	foreach (var karta in Karty)
    {
        Console.WriteLine(karta);  // WYPISANIE WSZYSTKICH KART
    }
    */

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

    Pojedynek(gracze, kozerKolor);
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



	
