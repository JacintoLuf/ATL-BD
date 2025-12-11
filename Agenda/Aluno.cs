using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Collections;
using System.Data;
using System.Diagnostics;

namespace Agenda {
    [Serializable()]
    class Aluno {
		private String _id;
		private String _name;
		private String _address;
		private String _nasc;
		private String _problems;
		private String _medication;
		private String _restriction;
		private String _antipiretico;

		public String Id {
			get { return _id; }
			set { _id = value; }
		}
		public String Name {
			get { return _name; }
			set{ _name = value; }
		}
		public String Address {
			get { return _address; }
			set { _address = value; }
		}
		public String Nasc {
			get { return _nasc; }
			set { _nasc = value; }
		}
		public String Problems {
			get { return _problems; }
			set { _problems = value; }
		}
		public String Medication {
			get { return _medication; }
			set { _medication = value; }
		}
		public String Restriction {
			get { return _restriction; }
			set { _restriction = value; }
		}
		public String Antipiretico {
			get { return _antipiretico; }
			set { _antipiretico = value; }
		}
		public override String ToString()
		{
			return _id+"   "+_name;
		}
	}
}
