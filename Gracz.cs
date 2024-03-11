using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrawBETA
{
	internal class Gracz
	{
		public Gracz() { }

		private List<string> karty = new List<string>();

		public void Karty(string Karta1, string Karta2, string Karta3)
		{
			karty.Clear(); // Usunięcie poprzednich kart, jeśli istniały
			karty.Add(Karta1);
			karty.Add(Karta2);
			karty.Add(Karta3);
		}

		public List<string> getKarty()
		{
			return karty;
		}

		public void aktualizujKarty(List<string> karty1)
		{
			karty = karty1;
		}

		public void pokazKarty()
		{
				Console.WriteLine(string.Join(" || " , karty));
		}


	}
}
