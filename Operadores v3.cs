using System;

namespace Operadores
{
    public class Fraccion
	{
		public int Entero      { get; set; }
		public int Numerador   { get; set; }
        public int Denominador { get; set; }

		public Fraccion Simplificar()
		{
			int i = 2;
			for (i = 2; i <= 13; i++)
			{
				if(this.Numerador % i == 0 && this.Denominador % i == 0)
				{
					this.Numerador   /= i;
					this.Denominador /= i;

					break;
				}
			}

			if (i == 14)
				return this;

			return this.Simplificar();
		}
	}


	public class Complejo
	{
		public float Real       { get; set; }
		public float Imaginario { get; set; }

		public override string ToString()
		{
			string salida = String.Empty;
			if (Real == 0f)
				salida = String.Format("{0}i", Imaginario);
			else if (Imaginario == 0f)
				salida = String.Format("{0}", Real);
			else
				salida = String.Format("{0}{1}{2}i", 
				                       Real,
				                       Imaginario < 0f ? "-" : "+",
				                       Math.Abs(Imaginario));       
			return salida;
		}
        
        
		public override bool Equals(object obj)
		{
			return this.Imaginario == (obj as Complejo).Imaginario &&
				   this.Real       == (obj as Complejo).Real ? true : false;
		}

		public static Complejo operator +(Complejo a,Complejo b)
		{
			return new Complejo() { Real = a.Real + b.Real,
				Imaginario = a.Imaginario + b.Imaginario};
			
		}
		public static Complejo operator -(Complejo a, Complejo b)
        {
            return new Complejo()
            {
                Real = a.Real - b.Real,
                Imaginario = a.Imaginario - b.Imaginario
            };

        }
		public static Complejo operator *(Complejo a, Complejo b)
        {
			return new Complejo()
			{
				Real = (a.Real * b.Real) - (a.Imaginario * b.Imaginario),
				Imaginario = (a.Real * b.Imaginario) + (a.Imaginario * b.Real)
            };

        }
		public static Complejo operator *(float escalar, Complejo b)
        {
            return new Complejo()
            {
                Real = (escalar * b.Real),
                Imaginario = (escalar * b.Imaginario) 
            };

        }
		public static Complejo operator *(Complejo b,float escalar)
        {
            return new Complejo()
            {
                Real = (escalar * b.Real),
                Imaginario = (escalar * b.Imaginario)
            };

        }

		public static bool operator ==(Complejo a,Complejo b) 
		{
			return a.Imaginario == b.Imaginario &&
                   a.Real       == b.Real ? true : false;
		}
		public static bool operator !=(Complejo a, Complejo b)
        {
			return a.Imaginario != b.Imaginario ||
                   a.Real != b.Real ? true : false;
        }
	}
    class MainClass
    {
		
        public static void Main(string[] args)
        {
			Complejo a = new Complejo() { Real = 12.34f, Imaginario = -45.67f };
			Complejo b = new Complejo() { Real = 12.3f, Imaginario = -45.67f };

			if (!a.Equals (b))
				Console.WriteLine("Son iguales");


			Console.WriteLine( (65.7f*(a*b)+(a-b)*b)*5);

			Fraccion fraccion = new Fraccion { Numerador = 9750, Denominador = 4500 };
			var x = fraccion.Simplificar();

        }
    }
}
