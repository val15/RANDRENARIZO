using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjM2RBF
{
    class ReseauRadiale
    {
        private static Random rnd = null;// pour génerer les nombres aleatoirs
        private int nbEntrEes;
        private int nbNoeudsCachEs;
        private int nbSorties;
        private Ligne entrEes;
        private Matrice centreDeGravitEe;
        private Ligne ecartsTypes;
        private Matrice poids;
        private Ligne biais;
        private Ligne sorties;

        //pour l'entrainement
        private Matrice matriceDesDonneesDeTests;
        private Matrice matriceDesDonneesDEntrainement;
        public ReseauRadiale(int nbEntrEes, int nbNoeudsCachEs, int nbSorties)
        {

            rnd = new Random(0);
            this.nbEntrEes = nbEntrEes;
            this.nbNoeudsCachEs = nbNoeudsCachEs;
            this.nbSorties = nbSorties;

            this.EntrEes = new Ligne(nbEntrEes);//la matrice d'entrée, matrice à une linge
            this.CentreDeGravitEe = new Matrice(this.nbNoeudsCachEs, this.nbEntrEes);//une Matrice(un cetre de gravite pour chaque noeud cachée et chaque cetre à le meme nombre de valeur que le nombre de l'entrées
            this.EcartsTypes = new Ligne(nbNoeudsCachEs);//écarts-types le nombre est égale au nombre de noeud cachées
            this.Poids = new Matrice(nbNoeudsCachEs, nbSorties);//une matrice, il y des poids de meme nobre de sortie
            this.Biais = new Ligne(nbSorties);//égale au nobre de sortie
            this.sorties = new Ligne(nbSorties);
        }

        internal Matrice CentreDeGravitEe { get => centreDeGravitEe; set => centreDeGravitEe = value; }
        internal Ligne EcartsTypes { get => ecartsTypes; set => ecartsTypes = value; }
        internal Matrice Poids { get => poids; set => poids = value; }
        internal Ligne Biais { get => biais; set => biais = value; }
        internal Ligne EntrEes { get => entrEes; set => entrEes = value; }

        public void afficherInfo()
        {
            Console.WriteLine("nb etrées : " + this.nbEntrEes);
            Console.WriteLine("nb noeuds cachés : " + this.nbNoeudsCachEs);
            Console.WriteLine("nb sorties : " + this.nbSorties);
            Console.WriteLine("entrées : ");
            this.EntrEes.afficher();
            Console.WriteLine("centre de gravitée : ");
            this.CentreDeGravitEe.afficher();
            Console.WriteLine("ecarts types : ");
            this.EcartsTypes.afficher();
            Console.WriteLine("poids : ");
            this.Poids.afficher();
            Console.WriteLine("biais : ");
            this.Biais.afficher();
            Console.WriteLine("sorties : ");
            this.sorties.afficher();
        }


        public void SetEcartTypePoidsBiais(double[] weights)
        {

            if (weights.Length != (this.nbNoeudsCachEs * this.nbSorties) + this.nbSorties)
                throw new Exception("Bad weights length in SetWeights");
            int k = 0; // ptr into weights
            for (int i = 0; i < this.nbNoeudsCachEs; ++i)
                for (int j = 0; j < this.nbSorties; ++j)
                    this.Poids.setE(i, j, weights[k++]); 
            for (int i = 0; i < this.nbSorties; ++i)
                this.Biais.setE(i, weights[k++]);
        }

        public void traiter()
        {

            //pour on calcule la sortie pou chaque noeud cachE
            Ligne ligneSortieNoeudCachE = new Ligne(this.nbNoeudsCachEs);//contien la sortie de tout les neouds coachés
            for (int j = 0; j < this.nbNoeudsCachEs; ++j) // pour chaque noeud
            {
                double d = Distance(this.entrEes, this.centreDeGravitEe.getL(j)); 
                                                                                  
               // Console.WriteLine("noeudCache[" + j + "] distance = " + d);
                double r = -1.0 * Math.Pow(d, 2) / (2 * Math.Pow(this.ecartsTypes.getE(j), 2));
                double g = Math.Exp(r);
              //  Console.WriteLine("noeudCache[" + j + "] output = " + g);
                ligneSortieNoeudCachE.setE(j, g);
            }


            for (int k = 0; k < this.nbSorties; ++k)
            {
                for (int j = 0; j < this.nbNoeudsCachEs; ++j)
                {
                    double gp = ligneSortieNoeudCachE.getE(j) * this.poids.getE(j, k);
                    this.sorties.setE(k, this.sorties.getE(k) + gp);
                }
             //   Console.WriteLine("sortie [" + k + "] = " + this.sorties.getE(k));
            }





            for (int k = 0; k < this.nbSorties; ++k)
                this.sorties.setE(k, this.sorties.getE(k) + this.biais.getE(k));


        }


        private double Distance(Ligne x, Ligne c)//cette methode calcule la distance
        {
            double sum = 0.0;
            for (int i = 0; i < x.Taille; ++i)
            {
                sum += Math.Pow((x.getE(i) - c.getE(i)), 2);
            }
            return Math.Sqrt(sum);
        }



        public void creerDonneesDEntraitemenEtTest(Matrice matriceDeDonneeBrute, int seed)
        {
            // 80% pour la matrice d'entrainement et 20% pour la matrice de  teste 
            int[] toutLesIndices = new int[matriceDeDonneeBrute.MI];// un tabeau de int qui contien tout les indices
            for (int i = 0; i < toutLesIndices.Length; ++i)
                toutLesIndices[i] = i;//on rempli le tableau

            Random rnd = new Random(seed);
            for (int i = 0; i < toutLesIndices.Length; ++i) // echange des indices
            {
                int r = rnd.Next(i, toutLesIndices.Length);
                int tmp = toutLesIndices[r];
                toutLesIndices[r] = toutLesIndices[i];
                toutLesIndices[i] = tmp;
            }

            int numTrain = (int)(0.80 * matriceDeDonneeBrute.MI);
            int numTest = matriceDeDonneeBrute.MI - numTrain;

            this.matriceDesDonneesDEntrainement = new Matrice(numTrain, matriceDeDonneeBrute.MJ);
            this.matriceDesDonneesDeTests = new Matrice(numTest, matriceDeDonneeBrute.MJ);



            int j = 0;
            for (int i = 0; i < numTrain; ++i)
                this.matriceDesDonneesDEntrainement.setL(i, matriceDeDonneeBrute.getL(toutLesIndices[j++])); 
            for (int i = 0; i < numTest; ++i)
                this.matriceDesDonneesDeTests.setL(i, matriceDeDonneeBrute.getL(toutLesIndices[j++])); 


            Console.WriteLine("matrice d'entrainement : ");
            this.matriceDesDonneesDEntrainement.afficher();
            Console.WriteLine("matrice de teste : ");
            this.matriceDesDonneesDeTests.afficher();
        }


        public void entrainerLeReseau()
        {

            genererCentroids();//cette fonction utilse la matrice des données d'entrainnements
            genererEcartType();//cette fonction ulilise les centres de gravitée
            int maxIterations = 100;
            generationDesPoidsEtDesBiais(maxIterations);//utilisatio de l'algo "PSO" 


        }

        private void genererCentroids()
        {
            int nbDonneesDEntrainnement = this.matriceDesDonneesDEntrainement.MI;
            int[] meilleursIndices = new int[this.nbNoeudsCachEs];  // tableau qui contiendra les meilleurs indices
            double maxAvgDistance = double.MinValue; // detérmination de la distance maximale
            for (int i = 0; i < nbDonneesDEntrainnement; ++i)
            {
                int[] indicesAleatoirs = DistinctIndices(this.nbNoeudsCachEs, matriceDesDonneesDEntrainement.MI); // candidate indices avec l’algo d'« échantillonnage de réservoir »
                double sumDists = 0.0; // la semme de tout les distances
                for (int j = 0; j < indicesAleatoirs.Length - 1; ++j) 
                {
                    int firstIndex = indicesAleatoirs[j];
                    int secondIndex = indicesAleatoirs[j + 1];
                    sumDists += AvgAbsDist(this.matriceDesDonneesDEntrainement.getL(firstIndex), this.matriceDesDonneesDEntrainement.getL(secondIndex), this.nbEntrEes); // distance "Avg"
                }

                double moyAvgDist = sumDists / this.nbEntrEes; //  éstimation de la distance en faisant la moyenne
                if (moyAvgDist > maxAvgDistance) // 
                {
                    maxAvgDistance = moyAvgDist;
                    Array.Copy(indicesAleatoirs, meilleursIndices, indicesAleatoirs.Length); // on copie indicesAleatoirs dans meilleursIndices
                }
            }

            //on construit les centroides à partir de "meilleursIndices"
            for (int i = 0; i < this.nbEntrEes; ++i)
            {
                int idx = meilleursIndices[i]; // on prend dans la ligne des meilleurs indices
                for (int j = 0; j < this.nbEntrEes; ++j)
                {
                    this.centreDeGravitEe.setE(i, j, this.matriceDesDonneesDEntrainement.getE(idx, j)); // [i][j] = trainData[idx][j]; // make a copy of values
                }
            }

            Console.WriteLine("centre de gravitée : ");
            this.centreDeGravitEe.afficher();

        }






        private int[] DistinctIndices(int n, int range)//"échantillonnage de réservoir" 
        {
            int[] result = new int[n];
            for (int i = 0; i < n; ++i)
                result[i] = i;

            for (int t = n; t < range; ++t)
            {
                int m = rnd.Next(0, t + 1);
                if (m < n) result[m] = t;
            }
            return result;
        }

        private static double AvgAbsDist(Ligne v1, Ligne v2, int numTerms)//methodes pour calculer la distance « Avg » 
        {
            if (v1.Taille != v2.Taille)
                throw new Exception("Vector lengths not equal in AvgAbsDist()");
            double sum = 0.0;
            for (int i = 0; i < numTerms; ++i)
            {
                double delta = Math.Abs(v1.getE(i) - v2.getE(i));
                sum += delta;
            }
            return sum / numTerms;
        }


        private void genererEcartType()
        {
            double sommeDesDistances = 0.0;
            int ct = 0; // pour calculer le nombre de pair
            for (int i = 0; i < this.centreDeGravitEe.MI - 2; ++i)
            {
                for (int j = i + 1; j < this.centreDeGravitEe.MI - 1; ++j)
                {
                    double dist = Distance(this.centreDeGravitEe.getL(i), this.centreDeGravitEe.getL(j));// calucle la distence ecludienne, le meme methode que dans la fonction de base
                    sommeDesDistances += dist;
                    ++ct;
                }
            }
            double avgDist = sommeDesDistances / ct;//la distance est la somme des distances EuCludienne divises par le nombre de centroid 

            for (int i = 0; i < this.EcartsTypes.Taille; ++i) 
                this.EcartsTypes.setE(i, avgDist); //on assigne les écart types

            Console.WriteLine("ecart type = ");
            this.EcartsTypes.afficher();
        }


        private void generationDesPoidsEtDesBiais(int maxIterations)// cette méthode est asser complexe, elle utilise l' alogo "PSO" 
        {
            // use PSO to find weights and bias values that produce a RBF network
            // that best matches training data
            int numberParticles = this.matriceDesDonneesDEntrainement.MI;//trainData.Length / 3;

            int Dim = (this.nbNoeudsCachEs * this.nbSorties) + this.nbSorties;// (numHidden * this.nbSorties) + this.nbSorties; // dimensions is num weights + num biases
            double minX = -10.0; // implicitly assumes data has been normalizzed
            double maxX = 10.0;
            double minV = minX;
            double maxV = maxX;
            Particle[] swarm = new Particle[numberParticles];
            double[] bestGlobalPosition = new double[Dim]; // best solution found by any particle in the swarm. implicit initialization to all 0.0
            double smallesttGlobalError = double.MaxValue; // smaller values better

            // initialize swarm
            for (int i = 0; i < swarm.Length; ++i) // initialize each Particle in the swarm
            {
                double[] randomPosition = new double[Dim];
                for (int j = 0; j < randomPosition.Length; ++j)
                {
                    double lo = minX;
                    double hi = maxX;
                    randomPosition[j] = (hi - lo) * rnd.NextDouble() + lo; // 
                }

                double err = MeanSquaredError(this.matriceDesDonneesDEntrainement.getTableau(), randomPosition); //MeanSquaredError(trainData, randomPosition); // error associated with the random position/solution
                double[] randomVelocity = new double[Dim];

                for (int j = 0; j < randomVelocity.Length; ++j)
                {
                    double lo = -1.0 * Math.Abs(maxV - minV);
                    double hi = Math.Abs(maxV - minV);
                    randomVelocity[j] = (hi - lo) * rnd.NextDouble() + lo;
                }
                swarm[i] = new Particle(randomPosition, err, randomVelocity, randomPosition, err);

                // does current Particle have global best position/solution?
                if (swarm[i].error < smallesttGlobalError)
                {
                    smallesttGlobalError = swarm[i].error;
                    swarm[i].position.CopyTo(bestGlobalPosition, 0);
                }
            } // initialization

            // main PSO algorithm
            // compute new velocity -> compute new position -> check if new best

            double w = 0.729; // inertia weight
            double c1 = 1.49445; // cognitive/local weight
            double c2 = 1.49445; // social/global weight
            double r1, r2; // cognitive and social randomizations

            int[] sequence = new int[numberParticles]; // process particles in random order
            for (int i = 0; i < sequence.Length; ++i)
                sequence[i] = i;

            int iteration = 0;
            while (iteration < maxIterations)
            {
                if (smallesttGlobalError < 0.060) break; // early exit (MSE)

                double[] newVelocity = new double[Dim]; // step 1
                double[] newPosition = new double[Dim]; // step 2
                double newError; // step 3

                Shuffle(sequence); // move particles in random sequence

                for (int pi = 0; pi < swarm.Length; ++pi) // each Particle (index)
                {
                    int i = sequence[pi];
                    Particle currP = swarm[i]; // for coding convenience

                    // 1. compute new velocity
                    for (int j = 0; j < currP.velocity.Length; ++j) // each x value of the velocity
                    {
                        r1 = rnd.NextDouble();
                        r2 = rnd.NextDouble();

                        // velocity depends on old velocity, best position of parrticle, and 
                        // best position of any particle
                        newVelocity[j] = (w * currP.velocity[j]) +
                          (c1 * r1 * (currP.bestPosition[j] - currP.position[j])) +
                          (c2 * r2 * (bestGlobalPosition[j] - currP.position[j]));

                        if (newVelocity[j] < minV)
                            newVelocity[j] = minV;
                        else if (newVelocity[j] > maxV)
                            newVelocity[j] = maxV;     // crude way to keep velocity in range
                    }

                    newVelocity.CopyTo(currP.velocity, 0);

                    // 2. use new velocity to compute new position
                    for (int j = 0; j < currP.position.Length; ++j)
                    {
                        newPosition[j] = currP.position[j] + newVelocity[j];  // compute new position
                        if (newPosition[j] < minX)
                            newPosition[j] = minX;
                        else if (newPosition[j] > maxX)
                            newPosition[j] = maxX;
                    }

                    newPosition.CopyTo(currP.position, 0);

                    // 3. use new position to compute new error
                    // consider cross-entropy error instead of MSE
                    newError = MeanSquaredError(this.matriceDesDonneesDEntrainement.getTableau(), newPosition); // makes next check a bit cleaner
                    currP.error = newError;

                    if (newError < currP.smallestError) // new particle best?
                    {
                        newPosition.CopyTo(currP.bestPosition, 0);
                        currP.smallestError = newError;
                    }

                    if (newError < smallesttGlobalError) // new global best?
                    {
                        newPosition.CopyTo(bestGlobalPosition, 0);
                        smallesttGlobalError = newError;
                    }

                    // consider using weight decay, particle death here

                } // each Particle

                ++iteration;

            } // while (main PSO processing loop)

            //Console.WriteLine("\n\nFinal training MSE = " + smallesttGlobalError.ToString("F4") + "\n\n");

            // copy best weights found into RBF network, and also return them
            // this.SetWeights(bestGlobalPosition);ecart-type
            SetEcartTypePoidsBiais(bestGlobalPosition);

            double[] returnResult = new double[(this.nbNoeudsCachEs * this.nbSorties) + this.nbSorties];
            Array.Copy(bestGlobalPosition, returnResult, bestGlobalPosition.Length);

            /*Console.WriteLine("The best weights and bias values found are:\n");
            Helpers.ShowVector(bestGlobalPosition, 3, 10, true);*/
            //  return returnResult;
        }

        private static void Shuffle(int[] sequence)
        {
            // helper for DoWeights to process particles in random order
            for (int i = 0; i < sequence.Length; ++i)
            {
                int r = rnd.Next(i, sequence.Length);
                int tmp = sequence[r];
                sequence[r] = sequence[i];
                sequence[i] = tmp;
            }
        }

        private double MeanSquaredError(double[][] trainData, double[] weights)
        {
            // assumes that centroids and widths have been set!
            //  this.SetWeights(weights); // copy the weights to valuate in
            SetEcartTypePoidsBiais(weights);



            double[] xValues = new double[this.nbEntrEes];// this.nbEntrEes]; // inputs
            double[] tValues = new double[this.nbSorties]; // targets
            double sumSquaredError = 0.0;
            for (int i = 0; i < trainData.Length; ++i) // walk through each trainingb data item
            {
                // following assumes data has all x-values first, followed by y-values!
                Array.Copy(trainData[i], xValues, this.nbEntrEes); // extract inputs
                Array.Copy(trainData[i], this.nbEntrEes, tValues, 0, this.nbSorties); // extract targets
                double[] yValues = this.ComputeOutputs(xValues); // compute the outputs using centroids, widths, weights, bias values
                for (int j = 0; j < yValues.Length; ++j)
                    sumSquaredError += ((yValues[j] - tValues[j]) * (yValues[j] - tValues[j]));
            }
            return sumSquaredError / trainData.Length;
        }


        public double[] ComputeOutputs(double[] xValues)
        {
            // use centroids, widths, weights and input xValues to compute, store, and return numOutputs output values
            Array.Copy(xValues, this.entrEes.getTableau(), xValues.Length); // place data inputs into RBF net inputs

            double[] hOutputs = new double[this.nbNoeudsCachEs]; // hidden node outputs
            for (int j = 0; j < this.nbNoeudsCachEs; ++j) // each hidden node
            {
                double d = Distance(this.entrEes, this.centreDeGravitEe.getL(j)); // could use a 'distSquared' approach
                                                                               //Console.WriteLine("\nHidden[" + j + "] distance = " + d.ToString("F4"));
                double r = -1.0 * (d * d) / (2 * this.EcartsTypes.getE(j) * this.EcartsTypes.getE(j));
                double g = Math.Exp(r);
                //Console.WriteLine("Hidden[" + j + "] output = " + g.ToString("F4"));
                hOutputs[j] = g;
            }

            double[] tempResults = new double[this.nbSorties];

            for (int k = 0; k < this.nbSorties; ++k)
                for (int j = 0; j < this.nbNoeudsCachEs ; ++j)
                    tempResults[k] += (hOutputs[j] * this.poids.getE(j,k)); // accumulate

            for (int k = 0; k < this.nbSorties; ++k)
                tempResults[k] += this.biais.getE(k); // add biases

            double[] finalOutputs = Softmax(tempResults); // scale the raw output so values sum to 1.0

            //Console.WriteLine("outputs:");
            //Helpers.ShowVector(finalOutputs, 3, finalOutputs.Length, true);
            //Console.ReadLine();

            Array.Copy(finalOutputs, this.sorties.getTableau(), finalOutputs.Length); // transfer computed outputs to RBF net outputs

            double[] returnResult = new double[this.nbSorties]; // also return computed outputs for convenience
            Array.Copy(finalOutputs, returnResult, this.sorties.getTableau().Length);
            return returnResult;
        } // ComputeOutputs


        private static double[] Softmax(double[] rawOutputs)
        {
            // helper for ComputeOutputs
            // does all output nodes at once so scale doesn't have to be re-computed each time
            // determine max output sum
            double max = rawOutputs[0];
            for (int i = 0; i < rawOutputs.Length; ++i)
                if (rawOutputs[i] > max) max = rawOutputs[i];

            // determine scaling factor -- sum of exp(each val - max)
            double scale = 0.0;
            for (int i = 0; i < rawOutputs.Length; ++i)
                scale += Math.Exp(rawOutputs[i] - max);

            double[] result = new double[rawOutputs.Length];
            for (int i = 0; i < rawOutputs.Length; ++i)
                result[i] = Math.Exp(rawOutputs[i] - max) / scale;

            return result; // now scaled so that all values sum to 1.0
        }
    }


    
}
