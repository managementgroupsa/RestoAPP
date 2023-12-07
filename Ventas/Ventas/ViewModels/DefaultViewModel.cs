using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using Ventas.Extensions;
using Ventas.Models;
using Ventas.Views;
using Xamarin.Forms;
using ZXing.Aztec.Internal;

namespace Ventas.ViewModels
{
    internal class DefaultViewModel : INotifyPropertyChanged
    {
        string tiemposesion;
        string tiempoinactividad;
        

        DateTime dateTimeInicial;
        DateTime dateTimeInicialActividad;
        DateTime dateTimeActual;


        public string TiempoSesion
        {
            get
            {
                return tiemposesion;
            }
            set
            {
                if (tiemposesion != value)
                {
                    tiemposesion = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("TiempoSesion"));
                    }
                }
            }
        }

        public string TiempoInactividad
        {
            get
            {
                return tiempoinactividad;
            }
            set
            {
                if (tiempoinactividad != value)
                {
                    tiempoinactividad = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("TiempoInactividad"));
                    }
                }
            }
        }

        public DefaultViewModel()
        {
            dateTimeInicial = DateTime.Now;

            Application.Current.Properties["TiempoInactividad"] = DateTime.Now;

           

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {

                dateTimeInicialActividad = Convert.ToDateTime( Application.Current.Properties["TiempoInactividad"] );

                dateTimeActual = DateTime.Now;

                TimeSpan DiferenciaSesion = dateTimeActual - dateTimeInicial;
                this.TiempoSesion = DiferenciaSesion.Hours.ToString("D2") + ":" + DiferenciaSesion.Minutes.ToString("D2") + ":" + DiferenciaSesion.Seconds.ToString("D2");

                TimeSpan DiferenciaInactividad= dateTimeActual - dateTimeInicialActividad;
                this.TiempoInactividad = DiferenciaInactividad.Minutes.ToString("D2") + ":" + DiferenciaInactividad.Seconds.ToString("D2");


                string cToken = Application.Current.Properties["Token"] as string;

                if (DiferenciaInactividad.Minutes >= 59 && !string.IsNullOrEmpty(cToken))
                {
                    
                    Application.Current.Properties["Token"] = "";


                    Shell.Current.GoToAsync(nameof(LoginPage));

                }

                if (DiferenciaSesion.Minutes >= 60)
                {

                    string cUsuario = Application.Current.Properties["Usu_cCodUsuario"] as string;
                    string cContraseña = Application.Current.Properties["Usu_cClave"] as string;

                    Login_Entity oUser = new Login_Entity();
                    oUser.Usu_cCodUsuario = cUsuario;
                    oUser.Usu_cClave = cContraseña;

                    string token = ProcedimientosAPI.GetPostBuscarToken(oUser);

                    Application.Current.Properties["Token"] = token;

                    dateTimeInicial = DateTime.Now;
                }

                return true;
            });

        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
