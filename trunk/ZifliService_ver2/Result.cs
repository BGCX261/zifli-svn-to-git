using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace ZifliService
{
    class Result
    {
        private float fltMean;

        private float fltSD;

        private ArrayList alValues;

        public void addValues(float[] fltValues)
        {
            alValues = new ArrayList();
            foreach (float fltValue in fltValues)
            {
                try
                {
                    this.alValues.Add(fltValue);
                    Logger.WriteEvent("Value added to ArrayList: " + fltValue.ToString(),
                        ZifliService._DEBUG);
                }
                catch
                {
                    Logger.WriteEvent("Could not add value to ArrayList alValues.",
                        ZifliService._ERR);
                }
            }
        }

        public ArrayList getALValues()
        {
            return this.alValues;
        }

        public void setMean(float fltMean)
        {
            this.fltMean = fltMean;
        }

        public void setSD(float fltSD)
        {
            this.fltSD = fltSD;
        }

        public float getMean()
        {
            return this.fltMean;
        }

        public float getSD()
        {
            return this.fltSD;
        }

        public static void computeMean(Result aResult)
        {
            //ArrayList ALValues = aResult.getALValues();
            //int j = ALValues.Count;
            float fltTotal = 0.0F;

            foreach (float fltValue in aResult.getALValues())
            {
                fltTotal += fltValue;
                Logger.WriteEvent("Total: " + fltTotal.ToString(), ZifliService._DEBUG);
            }
            aResult.setMean(fltTotal / aResult.getALValues().Count);
        }

        public static void computeSD(Result aResult)
        {
            float fltSum = 0.0F;
            float fltSummation = 0.0F;

            foreach (float fltValue in aResult.getALValues())
            {
                fltSum = (fltValue - aResult.getMean());
                Logger.WriteEvent("Sum: " + fltSum.ToString(), ZifliService._DEBUG);
                fltSum *= fltSum;
                Logger.WriteEvent("Sum^2: " + fltSum.ToString(), ZifliService._DEBUG);
                fltSummation += fltSum;
                Logger.WriteEvent("Summation: " + fltSum.ToString(), ZifliService._DEBUG);
            }
            aResult.setSD((float)Math.Sqrt(fltSummation / aResult.getALValues().Count));
        }
    }
}
