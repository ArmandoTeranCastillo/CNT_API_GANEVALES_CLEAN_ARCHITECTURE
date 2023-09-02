//-----------------------------------------------------------------------
// <copyright file="Curp.cs" company="">
//     Copyright (c). All rights reserved.
// </copyright>
// <author>Roberto Franco</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using UsersManagement.Common.Errors;
using UsersManagement.Common.Utilities;
using UsersManagement.CURP.Enums;

namespace UsersManagement.CURP
{
    public class Curp
    {
        private static readonly Dictionary<Estado, string> CodigosEstado = new Dictionary<Estado, string>
        {
            { Estado.Aguascalientes, "AS" },
            { Estado.BajaCalifornia, "BC" },
            { Estado.BajaCaliforniaSur, "BS" },
            { Estado.Campeche, "CC" },
            { Estado.Chiapas, "CS" },
            { Estado.Chihuahua, "CH" },
            { Estado.Coahuila, "CL" },
            { Estado.Colima, "CM" },
            { Estado.DistritoFederal, "DF" },
            { Estado.Durango, "DG" },
            { Estado.Guanajuato, "GT" },
            { Estado.Guerrero, "GR" },
            { Estado.Hidalgo, "HG" },
            { Estado.Jalisco, "JC" },
            { Estado.Mexico, "MC" },
            { Estado.Morelos, "MS" },
            { Estado.Michoacan, "MN" },
            { Estado.Nayarit, "NT" },
            { Estado.NuevoLeon, "NL" },
            { Estado.Oaxaca, "OC" },
            { Estado.Puebla, "PL" },
            { Estado.Queretaro, "QT" },
            { Estado.QuintanaRoo, "QR" },
            { Estado.SanLuisPotosi, "SP" },
            { Estado.Sinaloa, "SL" },
            { Estado.Sonora, "SR" },
            { Estado.Tabasco, "TC" },
            { Estado.Tamaulipas, "TS" },
            { Estado.Tlaxcala, "TL" },
            { Estado.Veracruz, "VZ" },
            { Estado.Yucatan, "YN" },
            { Estado.Zacatecas, "ZS" },
            { Estado.Extranjero, "NE" }
        };

        /// <summary>
        ///     Las Palabras inconvenientes.
        /// </summary>
        private static readonly HashSet<string> PalabrasInconvenientes = new HashSet<string>
        {
            "BACA", "BAKA", "BUEI", "BUEY",
            "CACA", "CACO", "CAGA", "CAGO", "CAKA", "CAKO", "COGE", "COGI", "COJA", "COJE", "COJI", "COJO", "COLA", "CULO",
            "FALO", "FETO",
            "GETA", "GUEI", "GUEY",
            "JETA", "JOTO",
            "KACA", "KACO", "KAGA", "KAGO", "KAKA", "KAKO", "KOGE", "KOGI", "KOJA", "KOJE", "KOJI", "KOJO", "KOLA", "KULO",
            "LILO", "LOCA", "LOCO", "LOKA", "LOKO",
            "MAME", "MAMO", "MEAR", "MEAS", "MEON", "MIAR", "MION", "MOCO", "MOKO", "MULA", "MULO",
            "NACA", "NACO",
            "PEDA", "PEDO", "PENE", "PIPI", "PITO", "POPO", "PUTA", "PUTO",
            "QULO",
            "RATA", "ROBA", "ROBE", "ROBO", "RUIN",
            "SENO",
            "TETA",
            "VACA", "VAGA", "VAGO", "VAKA", "VUEI", "VUEY",
            "WUEI", "WUEY"
        };

        /// <summary>
        ///     Genera la CURP.
        /// </summary>
        /// <param name="nombres"> Los nombres.</param>
        /// <param name="paterno"> El apellido paterno.</param>
        /// <param name="materno"> El apellido materno.</param>
        /// <param name="sexo"> El sexo.</param>
        /// <param name="fechaNacimiento"> La fecha de nacimiento.</param>
        /// <param name="estado"> El estado o entidad federativa de nacimiento.</param>
        /// <returns>La CURP.</returns>
        public static string Generar(string nombres, string paterno, string materno, Sexo sexo, DateTime fechaNacimiento, Estado estado)
        {
            try
            { // Aplicar filtros
                var nombreTemp = Filtrar(nombres);
                var paternoTemp = Filtrar(paterno);
                var maternoTemp = Filtrar(materno);

                // Posicion 1-4
                var uno = paternoTemp[0] == 'Ñ' ? 'X' : paternoTemp[0];
                var dos = paternoTemp.InternalVowel(1) ?? 'X';
                var tres = string.IsNullOrWhiteSpace(maternoTemp) ? 'X' : (maternoTemp[0] == 'Ñ' ? 'X' : maternoTemp[0]);
                var cuatro = nombreTemp[0] == 'Ñ' ? 'X' : nombreTemp[0];

                var fecha = $"{fechaNacimiento:yy}{fechaNacimiento.Month:D2}{fechaNacimiento.Day:D2}";
                var estadoCodigo = CodigosEstado[estado];

                // Posicion 14-16
                var x = paternoTemp.InternalConsonant(1);
                var y = maternoTemp?.InternalConsonant(1);
                var z = nombreTemp.InternalConsonant(1);

                var catorce = x == null ? 'X' : x == 'Ñ' ? 'X' : x;
                var quince = y == null ? 'X' : y == 'Ñ' ? 'X' : y;
                var dieciseis = z == null ? 'X' : z == 'Ñ' ? 'X' : z;

                // Pre CURP
                var preCurp = $"{uno}{dos}{tres}{cuatro}{fecha}{(char)sexo}{estadoCodigo}{catorce}{quince}{dieciseis}";

                // Reemplaza el 2do caracter por una X donde comience con alguna de las palabras de la lisa de "Palabras Inconvenientes"
                if (PalabrasInconvenientes.Contains(preCurp.Substring(0, 4)))
                {
                    preCurp = preCurp[0] + "X" + preCurp.Substring(2);
                }

                // Digito diferenciador de homonimia y siglo
                var diferenciador = fechaNacimiento.Year < 2000 ? "0" : "A";

                // Digito verificador
                var codigoVerificador = CodigoVerificador(preCurp);

                return $"{preCurp}{diferenciador}{codigoVerificador}";
            }
            catch (Exception)
            {
                throw new InternalServerError(Codes.FailedCurp);
            }     
        }

        /// <summary>
        ///     Calcula el codigo verificador en base a la pre CURP.
        /// </summary>
        /// <param name="preCurp"> La pre CURP.</param>
        /// <returns> El código verificador.</returns>
        /// <exception cref="ArgumentException"> Cuando alguno de los caracteres de la pre CURP no es válido.</exception>
        private static int CodigoVerificador(string preCurp)
        {
            var contador = 18;
            var sumatoria = 0;

            const string caracteres = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ";

            // Por cada caracter
            foreach (var caracter in preCurp)
            {
                var index = caracteres.IndexOf(caracter);

                if (index == -1)
                {
                    throw new ArgumentException($"Carácter inválido en la compisición de la pre CURP. [{caracter}]");
                }

                var valor = index * contador;
                contador--;
                sumatoria += valor;
            }

            // 12.- 2do digito verificador
            var numVer = sumatoria % 10;
            numVer = 10 - numVer;
            numVer = numVer == 10 ? 0 : numVer;

            return numVer;
        }

        /// <summary>
        ///     Aplica filtros a un texto en base a lo establecido para conformar la CURP.
        /// </summary>
        /// <param name="str">El texto a filtrar.</param>
        /// <returns> El texto resultante.</returns>
        private static string Filtrar(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return null;
            }

            // Nombres, paterno y materno en mayuscula
            str = str.ToUpper();

            // Eliminar acentos en vocales
            str = str.RemoveAccentMarks();

            // Eliminar dieresis en vocales
            str = str.RemoveVowelDieresis();

            // Criterios de excepcion
            var palabras = str.Split(' ')
                .Where(i => !string.IsNullOrWhiteSpace(i))
                .ToList();

            // Preposición, conjunción, contraccion
            var arr1 = new[] { "DA", "DAS", "DE", "DEL", "DER", "DI", "DIE", "DD", "EL", "LA", "LOS", "LAS", "LE", "LES", "MAC", "MC", "VAN", "VON", "Y", "J", "MA" };

            palabras = palabras.Where(i => !arr1.Contains(i))
                .ToList();

            // Nombre compuesto
            var arr2 = new[] { "MARIA", "MA.", "MA", "JOSE", "J", "J." };

            if (palabras.Count >= 2 && arr2.Contains(palabras[0]))
            {
                palabras.RemoveAt(0);
            }

            // Caracteres especiales
            str = palabras[0]
                .Replace('/', 'X')
                .Replace('-', 'X')
                .Replace('.', 'X');

            return str;
        }
    }
}
