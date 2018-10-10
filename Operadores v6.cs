using System;
using System.Text;

namespace Operadores
{
	public enum TipoDeSimplificacion
	{
		Reduccion,
        Decimal
	}

	public class App
	{
		static App()
		{
			App.TipoDeSimplificacion = TipoDeSimplificacion.Reduccion;
		} 
		public static TipoDeSimplificacion TipoDeSimplificacion { get; set; }
	}

    public class Fraccion
    {
        public decimal Entero      { get; set; }
		public decimal Numerador   { get; set; }
		public decimal Denominador { get; set; }

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

		public Fraccion SimplificarDecimal()
		{
			if(this.Numerador == 0 || this.Denominador == 0)
				return new Fraccion
                {
                    Denominador = 0,
                    Numerador   = 0
                };

			return new Fraccion
			{
				Denominador = 1000,
				Numerador = Math.Round( Math.Round(this.Numerador / this.Denominador, 3) * 1000,0) 
			};
		}

        public static Fraccion operator +(Fraccion a, Fraccion b)
        {
            if (a.Denominador == 0 && a.Numerador == 0)
                return b;
            
            if (b.Denominador == 0 && b.Numerador == 0)
                return a;
            
			return App.TipoDeSimplificacion == TipoDeSimplificacion.Decimal ?
            new Fraccion
            {
                Denominador = a.Denominador * b.Denominador,
                Numerador   = b.Numerador * a.Denominador + b.Denominador * a.Numerador
			}.SimplificarDecimal():
			 new Fraccion
			 {
				 Denominador = a.Denominador * b.Denominador,
				 Numerador = b.Numerador * a.Denominador + b.Denominador * a.Numerador
			 }.Simplificar();
			 
        }
        public static Fraccion operator -(Fraccion a, Fraccion b)
        {
            if (a.Denominador == 0 && a.Numerador == 0)
                return b;

            if (b.Denominador == 0 && b.Numerador == 0)
                return a;
            
			return App.TipoDeSimplificacion == TipoDeSimplificacion.Decimal ?
			new Fraccion
			{
				Denominador = a.Denominador * b.Denominador,
				Numerador = b.Denominador * a.Numerador - b.Numerador * a.Denominador
			}.SimplificarDecimal() :
			 new Fraccion
			 {
				 Denominador = a.Denominador * b.Denominador,
				 Numerador = b.Denominador * a.Numerador - b.Numerador * a.Denominador
			 }.Simplificar(); 
        }
        
        public static Fraccion operator *(Fraccion a, Fraccion b)
        {
			return App.TipoDeSimplificacion == TipoDeSimplificacion.Decimal ?
			new Fraccion
			{
				Denominador = a.Denominador * b.Denominador,
				Numerador = b.Numerador * a.Numerador
			}.SimplificarDecimal() :
			 new Fraccion
			 {
				 Denominador = a.Denominador * b.Denominador,
				 Numerador = b.Numerador * a.Numerador
			 }.Simplificar();
        }
        public static Fraccion operator /(Fraccion a, Fraccion b)
        {
			return App.TipoDeSimplificacion == TipoDeSimplificacion.Decimal ?
			new Fraccion
			{
				Numerador = a.Numerador * b.Denominador,
				Denominador = a.Denominador * b.Numerador
			}.SimplificarDecimal() :
			 new Fraccion
			 {
				 Numerador = a.Numerador * b.Denominador,
				 Denominador = a.Denominador * b.Numerador
			 }.Simplificar();
        }

        public override string ToString()
        {
            return String.Format("{0}/{1}",this.Numerador,this.Denominador);
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
    class Matriz
    {
        public int Renglones { get; set; }
        public int Columnas  { get; set; }

        private Fraccion[,] valores;

        public Matriz(int renglones, int columnas)
        {
            this.Renglones = renglones;
            this.Columnas = columnas;

            valores = new Fraccion[this.Renglones,this.Columnas];
        }
        
        //Indexador
        public Fraccion this[int renglon,int columna]
        {
            get
            {
                if (this.valores[renglon - 1,columna - 1] == null)
                    return new Fraccion();

                return this.valores[renglon - 1,columna - 1];
            }
            set
            {
				this.valores[renglon - 1,columna - 1] = value;
            }
        }
        
        public static Matriz operator +(Matriz a,Matriz b)
        {
            Matriz c = new Matriz(a.Renglones,a.Columnas);

            for (int i = 1; i <= a.Renglones; i++)
                for (int j = 1; j <= a.Columnas; j++)
                    c[i , j] = (a[i, j] + b[i, j]);

            return c;
        }
        
        public static Matriz operator -(Matriz a, Matriz b)
        {
            Matriz c = new Matriz(a.Renglones, a.Columnas);

            for (int i = 1; i <= a.Renglones; i++)
                for (int j = 1; j <= a.Columnas; j++)
                    c[i, j] = (a[i, j] - b[i, j]);

            return c;
        }
        public static Matriz operator *(Matriz a, Matriz b)
        {
            Matriz c = new Matriz(a.Renglones, b.Columnas);

            for (int i = 1; i <= a.Renglones; i++)
            {
                for (int j = 1; j <= b.Columnas; j++)
                {
                    Fraccion fraccion = new Fraccion();
                    for (int k = 1; k <= a.Columnas; k++)
                    {
                        var x = a[i, k] * b[k, j];
                        fraccion = fraccion + a[i, k] * b[k, j];                        
                    }

					c[i, j] = fraccion;
                }
            }         
           
            return c;
        }
        public static Matriz operator ^(Matriz a,int potencia)
        {
            Matriz c = a;

			if(potencia == -1)
				return a.Adjunta / a.Determinante;


            for (int i = 1; i <= potencia; i++)
                c = c * a;
            
            return c;
        }
		public static Matriz operator /(Matriz a, Fraccion divisor)
        {
			Matriz c = new Matriz(a.Columnas, a.Renglones);

			for (int i = 1; i <= a.Renglones; i++)
				for (int j = 1; j <= a.Columnas; j++)
					c[i, j] = a[i, j] / divisor;
              
            return c;
        }

		public static Matriz operator *(Matriz a, Fraccion multiplicador)
        {
            Matriz c = new Matriz(a.Columnas, a.Renglones);

            for (int i = 1; i <= a.Renglones; i++)
                for (int j  = 1; j <= a.Columnas; j++)
					c[i, j] = (a[i, j] * multiplicador);

            return c;
        }

		public Matriz Transpuesta
		{
			get
			{
				Matriz c = new Matriz(this.Columnas,this.Renglones);

				for (int i = 1; i <= this.Renglones; i++)
					for (int j = 1; j <= this.Columnas; j++)
						c[j, i] = this[i, j];

				return c;
			}
		}

		public Fraccion Determinante
		{
			get
			{
				if (this.Renglones == 1 && this.Columnas == 1)
					return this[1, 1];

				Fraccion resultado = new Fraccion();

				if (this.Renglones == 2 && this.Columnas == 2)
					return (this[1, 1] * this[2, 2]) - (this[2, 1] * this[1, 2]);


				for (int k = 1; k <= this.Columnas; k++)
					resultado += new Fraccion()
				    {
					    Numerador = (int)Math.Pow(-1, 1 + k),
					    Denominador = 1
				    }*this[1, k]
				     *this.Reducir(1, k).Determinante;


				return resultado;
			}
		}

		public Matriz Reducir(int renglon,int columna)
		{
			Matriz c = new Matriz(this.Renglones - 1, this.Columnas - 1);
			int w = 1;
			int z = 1;

			for (int i = 1; i <= this.Renglones; i++)
			{
				z = 1;
				if( i != renglon)
				{
					for (int j = 1; j <= this.Columnas; j++)
                    {
                        if (j != columna)
						{
							c[w, z] = this[i, j]; 
							z++;
						}
                    }
					w++;
				}
			}

			return c;
		}

		public Matriz Adjunta
        {
            get
            {
				Matriz c = new Matriz(this.Renglones, this.Columnas);

                for (int i = 1; i <= this.Renglones; i++)
				{
					for (int j = 1; j <= this.Columnas; j++)
					{
						Fraccion x = new Fraccion();
						x.Numerador = (int)Math.Pow((-1),(i + j));
						x.Denominador = 1;

						c[j, i] = x * this.Reducir(i, j).Determinante;
					}
				}
						

                return c;
            }
        }


            

        public override string ToString()
        {
            StringBuilder salida = new StringBuilder();
            
            for (int i = 1; i <= this.Renglones; i++)
            {
                for (int j = 1; j <= this.Columnas; j++)
                {
                    //salida.Append(this[i,j]);
                    //salida.Append(" ");

                    salida.AppendFormat("{0} ",this[i, j]);
                }
                salida.AppendLine(String.Empty);
            }
            
            return salida.ToString();
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

            Matriz w = new Matriz(3, 3);
            w[1, 1] = new Fraccion { Numerador = 2, Denominador = 1 };
            w[1, 2] = new Fraccion { Numerador = 4, Denominador = 1 };
            w[1, 3] = new Fraccion { Numerador = 6, Denominador = 1 };
            w[2, 1] = new Fraccion { Numerador = 4, Denominador = 1 };
            w[2, 2] = new Fraccion { Numerador = 5, Denominador = 1 };
            w[2, 3] = new Fraccion { Numerador = 6, Denominador = 1 };
            w[3, 1] = new Fraccion { Numerador = 3, Denominador = 1 };
            w[3, 2] = new Fraccion { Numerador = 1, Denominador = 1 };
            w[3, 3] = new Fraccion { Numerador = -2, Denominador = 1 };

			Matriz b2 = new Matriz(3, 1);
			b2[1, 1] = new Fraccion { Numerador = 18, Denominador = 1 };
            b2[2, 1] = new Fraccion { Numerador = 24, Denominador = 1 };
            b2[3, 1] = new Fraccion { Numerador =  4, Denominador = 1 };
         

            Matriz y = new Matriz(3, 3);
            
            y[1, 1] = new Fraccion { Numerador = 80, Denominador = 100 };
            y[1, 2] = new Fraccion { Numerador = 10, Denominador = 100 };
            y[1, 3] = new Fraccion { Numerador = 10, Denominador = 100 };
            y[2, 1] = new Fraccion { Numerador = 3, Denominador = 100 };
            y[2, 2] = new Fraccion { Numerador = 95, Denominador = 100 };
            y[2, 3] = new Fraccion { Numerador = 2, Denominador = 100 };
            y[3, 1] = new Fraccion { Numerador = 20, Denominador = 100 };
            y[3, 2] = new Fraccion { Numerador = 5, Denominador = 100 };
            y[3, 3] = new Fraccion { Numerador = 75, Denominador = 100 };

			Matriz z0 = new Matriz(1, 3);
			z0[1, 1] = new Fraccion { Numerador = 45, Denominador = 100 };
			z0[1, 2] = new Fraccion { Numerador = 25, Denominador = 100 };
			z0[1, 3] = new Fraccion { Numerador = 30, Denominador = 100 };


			//Matriz z = (w * y).Transpuesta.Reducir(1,1);

			//Matriz t   = w.Transpuesta;
			//Matriz r   = t.Reducir(1, 1);
			//Matriz adj = w.Adjunta;
			Fraccion     f = w.Determinante;
			Matriz inversa = (w ^ -1);
			//Matriz q = w ^ 5;

			Matriz resultante =  inversa * b2;

            
			Console.WriteLine(w);
			//Console.WriteLine(t);
            //Console.WriteLine(r);
			//Console.WriteLine(adj);
			//Console.WriteLine(f);
			Console.WriteLine(inversa);
			Console.WriteLine(resultante);

			App.TipoDeSimplificacion = TipoDeSimplificacion.Decimal;

            
			for (int i= 0; i < 100; i++)
			{
				z0 = z0 * y;  
                Console.WriteLine(z0);
			}

			App.TipoDeSimplificacion = TipoDeSimplificacion.Reduccion;




            
            //z = w ^ 5;
            
        }
    }
}
