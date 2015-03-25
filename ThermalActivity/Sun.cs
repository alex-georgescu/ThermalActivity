using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;


namespace ThermalActivity
{
    public class Sun
    {
        private uint _radius;
        private uint[] _rawData;
        private KeyValuePair<Tuple<uint, uint>, uint>[] _solarScores;

        public ObservableCollection<uint> HeatMap;


        public Sun(uint[] data)
        {
            _rawData = new uint[data.Length];
            _radius = (uint) Math.Sqrt(data.Length);

            for (uint i=0; i < data.Length; i++)
            {
                _rawData[i] = data[i];
            }
        }

        public async Task EvaluateSunSurface()
        {
            _solarScores = new KeyValuePair<Tuple<uint, uint>, uint>[_rawData.Length];
    
            TaskFactory threadFactory = new TaskFactory();
            List<Task> threads = new List<Task>(); 
            
            for (uint row = 0; row < _radius; row ++)
            {
                uint i = row;
                threads.Add(threadFactory.StartNew(() =>
                {
                    for (uint j = 0; j < _radius; j++)
                    {
                        EvalSolarScoreAtPosition(i, j);
                    }
                }));
            }

            await threadFactory.ContinueWhenAll(threads.ToArray(), obj => GenerateHeatMap());
        }


        private void EvalSolarScoreAtPosition(uint i, uint j)
        {
            uint solarScore = _rawData[i * _radius + j]
                                +
                                ((i > 0) ? _rawData[(i - 1) * _radius + j] : 0)
                                +
                                ((i > 0 && j > 0) ? _rawData[(i - 1) * _radius + j - 1] : 0)
                                +
                                ((i > 0 && j < _radius - 1) ? _rawData[(i - 1) * _radius + j + 1] : 0)
                                +
                                ((j < _radius - 1) ? _rawData[i * _radius + j + 1] : 0)
                                +
                                ((j > 0) ? _rawData[i * _radius + j - 1] : 0)
                                +
                                ((i < _radius - 1 && j > 0) ? _rawData[(i + 1) * _radius + j - 1] : 0)
                                +
                                ((i < _radius - 1) ? _rawData[(i + 1) * _radius + j] : 0)
                                +
                                ((i < _radius - 1 && j < _radius - 1) ? _rawData[(i + 1) * _radius + j + 1] : 0);

            _solarScores[i * _radius + j] = new KeyValuePair<Tuple<uint, uint>, uint>(new Tuple<uint, uint>(j, i), solarScore); 
            // Tuple(j,i) and not Tuple(i,j) because of the XoY coordinates system: 
            // i - points to rows, so to OY values and j - points to columns so to OX values
        }

        
        // Task 1
        public string GetTopSolarScores(uint t)
        {
            _solarScores = _solarScores.OrderByDescending(tuple => tuple.Value).ToArray();

            string output = string.Empty;
            for (uint i = 0; i < t; i++)
            {
                output += string.Format("({0},{1} score:{2})", _solarScores[i].Key.Item1, _solarScores[i].Key.Item2, _solarScores[i].Value);
            }

            return output;
        }


        //Task 2
        private void GenerateHeatMap()
        {
            HeatMap = new ObservableCollection<uint>();
            foreach (KeyValuePair<Tuple<uint, uint>, uint> kvp in _solarScores)
            {
                HeatMap.Add(kvp.Value);
            }
        }
    }
}
