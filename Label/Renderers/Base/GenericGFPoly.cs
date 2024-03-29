﻿

using System;
using System.Text;


namespace Label.Renderers.Base
{
    internal sealed class GenericGFPoly
    {
        private readonly GenericGF field;
        private readonly int[] coefficients;


        internal GenericGFPoly(GenericGF field, int[] coefficients)
        {
            if (coefficients.Length == 0)
            {
                throw new ArgumentException();
            }
            this.field = field;
            int coefficientsLength = coefficients.Length;
            if (coefficientsLength > 1 && coefficients[0] == 0)
            {
                // Leading term must be non-zero for anything except the constant polynomial "0"
                int firstNonZero = 1;
                while (firstNonZero < coefficientsLength && coefficients[firstNonZero] == 0)
                {
                    firstNonZero++;
                }
                if (firstNonZero == coefficientsLength)
                {
                    this.coefficients = new int[] { 0 };
                }
                else
                {
                    this.coefficients = new int[coefficientsLength - firstNonZero];
                    Array.Copy(coefficients,
                        firstNonZero,
                        this.coefficients,
                        0,
                        this.coefficients.Length);
                }
            }
            else
            {
                this.coefficients = coefficients;
            }
        }

        internal int[] Coefficients
        {
            get { return coefficients; }
        }

        /// <summary>
        /// degree of this polynomial
        /// </summary>
        internal int Degree
        {
            get
            {
                return coefficients.Length - 1;
            }
        }


        internal bool isZero
        {
            get { return coefficients[0] == 0; }
        }


        internal int getCoefficient(int degree)
        {
            return coefficients[coefficients.Length - 1 - degree];
        }


        internal int evaluateAt(int a)
        {
            int result = 0;
            if (a == 0)
            {
                // Just return the x^0 coefficient
                return getCoefficient(0);
            }
            if (a == 1)
            {
                // Just the sum of the coefficients
                foreach (var coefficient in coefficients)
                {
                    result = GenericGF.addOrSubtract(result, coefficient);
                }
                return result;
            }
            result = coefficients[0];
            int size = coefficients.Length;
            for (int i = 1; i < size; i++)
            {
                result = GenericGF.addOrSubtract(field.multiply(a, result), coefficients[i]);
            }
            return result;
        }

        internal GenericGFPoly addOrSubtract(GenericGFPoly other)
        {
            if (!field.Equals(other.field))
            {
                throw new ArgumentException("GenericGFPolys do not have same GenericGF field");
            }
            if (isZero)
            {
                return other;
            }
            if (other.isZero)
            {
                return this;
            }

            int[] smallerCoefficients = this.coefficients;
            int[] largerCoefficients = other.coefficients;
            if (smallerCoefficients.Length > largerCoefficients.Length)
            {
                int[] temp = smallerCoefficients;
                smallerCoefficients = largerCoefficients;
                largerCoefficients = temp;
            }
            int[] sumDiff = new int[largerCoefficients.Length];
            int lengthDiff = largerCoefficients.Length - smallerCoefficients.Length;
            // Copy high-order terms only found in higher-degree polynomial's coefficients
            Array.Copy(largerCoefficients, 0, sumDiff, 0, lengthDiff);

            for (int i = lengthDiff; i < largerCoefficients.Length; i++)
            {
                sumDiff[i] = GenericGF.addOrSubtract(smallerCoefficients[i - lengthDiff], largerCoefficients[i]);
            }

            return new GenericGFPoly(field, sumDiff);
        }

        internal GenericGFPoly multiply(GenericGFPoly other)
        {
            if (!field.Equals(other.field))
            {
                throw new ArgumentException("GenericGFPolys do not have same GenericGF field");
            }
            if (isZero || other.isZero)
            {
                return field.Zero;
            }
            int[] aCoefficients = this.coefficients;
            int aLength = aCoefficients.Length;
            int[] bCoefficients = other.coefficients;
            int bLength = bCoefficients.Length;
            int[] product = new int[aLength + bLength - 1];
            for (int i = 0; i < aLength; i++)
            {
                int aCoeff = aCoefficients[i];
                for (int j = 0; j < bLength; j++)
                {
                    product[i + j] = GenericGF.addOrSubtract(product[i + j],
                        field.multiply(aCoeff, bCoefficients[j]));
                }
            }
            return new GenericGFPoly(field, product);
        }

        internal GenericGFPoly multiply(int scalar)
        {
            if (scalar == 0)
            {
                return field.Zero;
            }
            if (scalar == 1)
            {
                return this;
            }
            int size = coefficients.Length;
            int[] product = new int[size];
            for (int i = 0; i < size; i++)
            {
                product[i] = field.multiply(coefficients[i], scalar);
            }
            return new GenericGFPoly(field, product);
        }

        internal GenericGFPoly multiplyByMonomial(int degree, int coefficient)
        {
            if (degree < 0)
            {
                throw new ArgumentException();
            }
            if (coefficient == 0)
            {
                return field.Zero;
            }
            int size = coefficients.Length;
            int[] product = new int[size + degree];
            for (int i = 0; i < size; i++)
            {
                product[i] = field.multiply(coefficients[i], coefficient);
            }
            return new GenericGFPoly(field, product);
        }

        internal GenericGFPoly[] divide(GenericGFPoly other)
        {
            if (!field.Equals(other.field))
            {
                throw new ArgumentException("GenericGFPolys do not have same GenericGF field");
            }
            if (other.isZero)
            {
                throw new ArgumentException("Divide by 0");
            }

            GenericGFPoly quotient = field.Zero;
            GenericGFPoly remainder = this;

            int denominatorLeadingTerm = other.getCoefficient(other.Degree);
            int inverseDenominatorLeadingTerm = field.inverse(denominatorLeadingTerm);

            while (remainder.Degree >= other.Degree && !remainder.isZero)
            {
                int degreeDifference = remainder.Degree - other.Degree;
                int scale = field.multiply(remainder.getCoefficient(remainder.Degree), inverseDenominatorLeadingTerm);
                GenericGFPoly term = other.multiplyByMonomial(degreeDifference, scale);
                GenericGFPoly iterationQuotient = field.buildMonomial(degreeDifference, scale);
                quotient = quotient.addOrSubtract(iterationQuotient);
                remainder = remainder.addOrSubtract(term);
            }

            return new GenericGFPoly[] { quotient, remainder };
        }

        public override String ToString()
        {
            if (isZero)
            {
                return "0";
            }
            StringBuilder result = new StringBuilder(8 * Degree);
            for (int degree = Degree; degree >= 0; degree--)
            {
                int coefficient = getCoefficient(degree);
                if (coefficient != 0)
                {
                    if (coefficient < 0)
                    {
                        if (degree == Degree)
                        {
                            result.Append("-");
                        }
                        else
                        {
                            result.Append(" - ");
                        }
                        coefficient = -coefficient;
                    }
                    else
                    {
                        if (result.Length > 0)
                        {
                            result.Append(" + ");
                        }
                    }
                    if (degree == 0 || coefficient != 1)
                    {
                        int alphaPower = field.log(coefficient);
                        if (alphaPower == 0)
                        {
                            result.Append('1');
                        }
                        else if (alphaPower == 1)
                        {
                            result.Append('a');
                        }
                        else
                        {
                            result.Append("a^");
                            result.Append(alphaPower);
                        }
                    }
                    if (degree != 0)
                    {
                        if (degree == 1)
                        {
                            result.Append('x');
                        }
                        else
                        {
                            result.Append("x^");
                            result.Append(degree);
                        }
                    }
                }
            }
            return result.ToString();
        }
    }
}
