using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrawBETA
{
	internal class PierwszaKartaRzucona
	{
		public bool CzyKartaRzucona = false;

		public bool CzyRzucona()
		{
			return CzyKartaRzucona;
		}

		public void ZostalaRzucona()
		{
			CzyKartaRzucona = true;
		}

		public void KoniecRozdania()
		{
			CzyKartaRzucona = false;
		}
	}
}
