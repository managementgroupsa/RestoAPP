using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RestoPLUS.ViewModels
{
    public class TableInfo : INotifyPropertyChanged
    {
        private string tableName;
        private string tableServer;
        private string tableCode;
        private string tableUser;
        private string pvt_cCodigo;
        private string res_cNummov;
        private string ped_cNummov;
        private string ent_cPersona;
        private string mes_cDescripcion;
        private string mes_cCodigo;
        private string ape_cUserCrea;

        private DateTime? ape_dFechaCrea;

        public string Ped_cNummov
        {
            get { return ped_cNummov; }
            set
            {
                ped_cNummov = value;
                OnPropertyChanged("Ped_cNummov");
            }
        }
        public string Res_cNummov
        {
            get { return res_cNummov; }
            set
            {
                res_cNummov = value;
                OnPropertyChanged("Res_cNummov");
            }
        }


        public string Pvt_cCodigo
        {
            get { return pvt_cCodigo; }
            set
            {
                pvt_cCodigo = value;
                OnPropertyChanged("Pvt_cCodigo");
            }
        }

        public string Ape_cUserCrea
        {
            get { return ape_cUserCrea; }
            set
            {
                ape_cUserCrea = value;
                OnPropertyChanged("Ape_cUserCrea");
            }
        }

        public string Mes_cCodigo
        {
            get { return mes_cCodigo; }
            set
            {
                mes_cCodigo = value;
                OnPropertyChanged("Mes_cCodigo");
            }
        }

        public string Mes_cDescripcion
        {
            get { return mes_cDescripcion; }
            set
            {
                mes_cDescripcion = value;
                OnPropertyChanged("Mes_cDescripcion");
            }
        }

        public string Ent_cPersona
        {
            get { return ent_cPersona; }
            set
            {
                ent_cPersona = value;
                OnPropertyChanged("Ent_cPersona");
            }
        }

        public DateTime? Ape_dFechaCrea
        {
            get { return ape_dFechaCrea; }
            set
            {
                if (ape_dFechaCrea != value)
                {
                    ape_dFechaCrea = value;
                    OnPropertyChanged(nameof(Ape_dFechaCrea));
                }
            }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

    }
}
