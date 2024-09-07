using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrawBETA
{
	internal class Kozer
	{
		string nazwakozer;
		public Kozer() { }	

		public Kozer(string nazwakozer1)
		{
			nazwakozer = nazwakozer1;
		}
		public string getKozer()
		{
			return nazwakozer;
		}

		public void pobierz(string wartosc)
		{
			nazwakozer = wartosc;
		}

		public void pokazKozer()
		{
            Console.WriteLine(nazwakozer);
        }
	}
}
