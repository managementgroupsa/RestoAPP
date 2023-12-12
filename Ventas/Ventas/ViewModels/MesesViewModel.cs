using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace RestoPLUS.ViewModels
{
	public class Meses
	{
		private string id;
		public string ID
		{
			get { return id; }
			set { id = value; }
		}
		private string name;
		public string Name
		{
			get { return name; }
			set { name = value; }
		}
	}

	public class MesesViewModel
	{
		private ObservableCollection<Meses> mesesCollection;
		public ObservableCollection<Meses> MesesCollection
		{
			get { return mesesCollection; }
			set { mesesCollection = value; }
		}
		public MesesViewModel()
		{
			mesesCollection = new ObservableCollection<Meses>();

			mesesCollection.Add(new Meses() { ID = "01", Name = "ENERO" });
			mesesCollection.Add(new Meses() { ID = "02", Name = "FEBRERO" });
			mesesCollection.Add(new Meses() { ID = "03", Name = "MARZO" });
			mesesCollection.Add(new Meses() { ID = "04", Name = "ABRIL" });
			mesesCollection.Add(new Meses() { ID = "05", Name = "MAYO" });
			mesesCollection.Add(new Meses() { ID = "06", Name = "JUNIO" });
			mesesCollection.Add(new Meses() { ID = "07", Name = "JULIO" });
			mesesCollection.Add(new Meses() { ID = "08", Name = "AGOSTO" });
			mesesCollection.Add(new Meses() { ID = "09", Name = "SETIEMBRE" });
			mesesCollection.Add(new Meses() { ID = "10", Name = "OCTUBRE" });
			mesesCollection.Add(new Meses() { ID = "11", Name = "NOVIEMBRE" });
			mesesCollection.Add(new Meses() { ID = "12", Name = "DICIEMBRE" });
		}
	}
}
