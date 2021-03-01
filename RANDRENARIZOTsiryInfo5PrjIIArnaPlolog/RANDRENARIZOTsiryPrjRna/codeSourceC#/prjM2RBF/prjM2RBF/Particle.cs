using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjM2RBF
{
    public class Particle //cette classe est utiliser pour l'algo "PSO" 
    {
        public double[] position; 
        public double error; 
        public double[] velocity;

        public double[] bestPosition; 
        public double smallestError;

        public Particle(double[] position, double error, double[] velocity, double[] bestPosition, double smallestError)
        {
            this.position = new double[position.Length];
            position.CopyTo(this.position, 0);
            this.error = error;
            this.velocity = new double[velocity.Length];
            velocity.CopyTo(this.velocity, 0);
            this.bestPosition = new double[bestPosition.Length];
            bestPosition.CopyTo(this.bestPosition, 0);
            this.smallestError = smallestError;
        }
    } 
}
