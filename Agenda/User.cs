using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Collections;
using System.Data;
using System.Diagnostics;

namespace Agenda
{
	[Serializable()]
	public class User
	{
		private String _usr;
		private String _pass;
		private String _usrType;

		public String Usr
		{
			get { return _usr; }
			set { _usr = value; }
		}
		public String Pass
		{
			get { return _pass; }
			set { _pass = value; }
		}
		public String UsrType
		{
			get { return _usrType; }
			set { _usrType = value; }
		}
	}
}
