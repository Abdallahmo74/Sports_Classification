using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiQueueModels
{
    public class SimulationSystem
    {
        public SimulationSystem()
        {
            this.Servers = new List<Server>();
            this.InterarrivalDistribution = new List<TimeDistribution>();
            this.PerformanceMeasures = new PerformanceMeasures();
            this.SimulationTable = new List<SimulationCase>();

            


        }

        ///////////// INPUTS ///////////// 
        public int NumberOfServers { get; set; }
        public int StoppingNumber { get; set; }
        public List<Server> Servers { get; set; }
        public List<TimeDistribution> InterarrivalDistribution { get; set; }
        public Enums.StoppingCriteria StoppingCriteria { get; set; }
        public Enums.SelectionMethod SelectionMethod { get; set; }

        ///////////// OUTPUTS /////////////
        public List<SimulationCase> SimulationTable { get; set; }
        public PerformanceMeasures PerformanceMeasures { get; set; }


        

        public void CalculatingCummProb(List<TimeDistribution> distribution)
        {
            int count = distribution.Count;
            for(int i = 0 ; i < count; i++)
            {
                for(int j = 0; j <= i ; j++)
                {
                    distribution[i].CummProbability += distribution[j].Probability;
                }
            }
        }


        public void CalculatingMinMaxRange(List<TimeDistribution> distribution)
        {
            int count = distribution.Count;
            for(int i = 0 ; i < count ; i++)
            {
                if (i == 0)
                {
                    distribution[i].MinRange = 1 ;
                    distribution[i].MaxRange = (int)(distribution[i].CummProbability*100);

                }
                else
                {
                    distribution[i].MinRange = (int)(distribution[i - 1].CummProbability * 100) + 1;
                    distribution[i].MaxRange = (int)(distribution[i].CummProbability * 100);

                }
            }

        }

        public void GeneratingRandomNumber(List<SimulationCase> simulationTable , int StoppingNumber)
        {
            Random rnd = new Random();

            for(int i =0; i < StoppingNumber ;i++)
            {
                
                simulationTable[i].CustomerNumber = i + 1;
                if (i == 0)
                {
                    simulationTable[i].RandomInterArrival = 1 ;
                    simulationTable[i].RandomService = rnd.Next(1, 100);
                }
                else
                {
                    simulationTable[i].RandomInterArrival = rnd.Next(1, 100);
                    simulationTable[i].RandomService = rnd.Next(1, 100);
                }
            }
        }
             

        
        public void TimeBetweenArrivals(List<SimulationCase> simulationTable , List<TimeDistribution> distribution , int StoppingNumber)
        {
            int count = distribution.Count;
            for (int i = 1; i < StoppingNumber; i++)
            { 
             for(int j = 0; j < count; j++)
                {
                    if((simulationTable[i].RandomInterArrival >= distribution[j].MinRange) && (simulationTable[i].RandomInterArrival <= distribution[j].MaxRange))
                    {
                        simulationTable[i].InterArrival = distribution[j].Time;
                    }
                }
            }
            simulationTable[0].InterArrival = 0;
        }



        public void TimeOfArrivals( List<SimulationCase> simulationTable , int StoppingNumber)
        {
            simulationTable[0].ArrivalTime = 0;
            for(int i = 1 ; i < StoppingNumber ;i++)
            {
                for(int j = 1 ; j <= i ; j++)
                {
                    simulationTable[i].ArrivalTime += simulationTable[j].InterArrival;
                }
            }
        }


        public void SetServiceTimeDistribution( List<Server> Servers )
        {
            int count = Servers.Count;
            
            //Cummulative probability for each server
            for (int i = 0; i < count; i++)
            {
                int Tcount = Servers[i].TimeDistribution.Count;
                for (int z = 0; z < Tcount; z++)
                {
                    for (int j = 0; j <= z; j++)
                    {
                        Servers[i].TimeDistribution[z].CummProbability += Servers[i].TimeDistribution[j].Probability;
                    }
                }
            }


            // Min and Max Range for each server
            for(int i = 0 ; i< count; i++)
            {
                int Tcount = Servers[i].TimeDistribution.Count;
                for(int j = 0 ; j < Tcount ; j++)
                {
                    if (j == 0)
                    {
                        Servers[i].TimeDistribution[j].MinRange = 1;
                        Servers[i].TimeDistribution[j].MaxRange = (int)(Servers[i].TimeDistribution[j].CummProbability* 100);

                    }
                    else
                    {
                        Servers[i].TimeDistribution[j].MinRange = (int)((Servers[i].TimeDistribution[j - 1].CummProbability) * 100) + 1;
                        Servers[i].TimeDistribution[j].MaxRange = (int)(Servers[i].TimeDistribution[j].CummProbability* 100);

                    }
                }

            }
        }



        public void SettingServersID( List<Server> Servers)
        {
            for(int i = 0; i< Servers.Count ; i++)
            {
                Servers[i].ID = i+1;
            }
        }


        public void GetServiceTime( List<SimulationCase> simulationTable ,  List<Server> Servers, int customerNum , int Servernum)
        {
            
            simulationTable[customerNum - 1].AssignedServer.ID = Servers[Servernum].ID;
            simulationTable[customerNum - 1].AssignedServer.TimeDistribution = Servers[Servernum].TimeDistribution;
            simulationTable[customerNum - 1].AssignedServerID = Servers[Servernum].ID;
            int count = simulationTable[customerNum - 1].AssignedServer.TimeDistribution.Count;
            for(int i = 0 ; i < count ; i++)
            {
                if ( (simulationTable[customerNum - 1].RandomService >= simulationTable[customerNum - 1].AssignedServer.TimeDistribution[i].MinRange)  && (simulationTable[customerNum - 1].RandomService <= simulationTable[customerNum - 1].AssignedServer.TimeDistribution[i].MaxRange) )
                {

                    simulationTable[customerNum - 1].ServiceTime = simulationTable[customerNum - 1].AssignedServer.TimeDistribution[i].Time;

                }
            }



        }

        //If The Selection Method is Highest Priority (1)
        public void HighestPriority(int CustomerNum , List<SimulationCase> simulationTable ,  List<Server> Servers )
        {
           
            bool full = true;
            if(CustomerNum == 1)
            {
                
                GetServiceTime( simulationTable, Servers, 1, 0);
                simulationTable[0].StartTime = 0;
                simulationTable[0].EndTime = simulationTable[0].ServiceTime;
                simulationTable[0].AssignedServer.FinishTime = simulationTable[0].ServiceTime;
                Servers[0].FinishTime = simulationTable[CustomerNum - 1].AssignedServer.FinishTime;


            }
            else
            {
                for (int i = 0 ; i < Servers.Count; i++)
                {
                    if (simulationTable[CustomerNum - 1].ArrivalTime >= Servers[i].FinishTime)
                    {
                        simulationTable[CustomerNum-1].AssignedServer.ID = Servers[i].ID;
                        simulationTable[CustomerNum - 1].AssignedServer.TimeDistribution = Servers[i].TimeDistribution;
                        simulationTable[CustomerNum - 1].StartTime = simulationTable[CustomerNum - 1].ArrivalTime;
                        GetServiceTime( simulationTable, Servers, CustomerNum , i);
                        simulationTable[CustomerNum - 1].EndTime = simulationTable[CustomerNum - 1].StartTime + simulationTable[CustomerNum - 1].ServiceTime;
                        simulationTable[CustomerNum-1].AssignedServer.FinishTime = simulationTable[CustomerNum - 1].EndTime;
                        Servers[i].FinishTime = simulationTable[CustomerNum - 1].AssignedServer.FinishTime;
                        full = false;
                        break;
                    }
                }


                
                if(full == true)
                {   int ServerNum = 0 ;
                    int min = 1000;
                    
                    for(int i = 0; i < Servers.Count; i++)
                    {
                        if(Servers[i].FinishTime < min)
                        {
                            min = Servers[i].FinishTime;
                            ServerNum = i;
                        }
                    }
                    simulationTable[CustomerNum-1].TimeInQueue = Servers[ServerNum].FinishTime - simulationTable[CustomerNum - 1].ArrivalTime;
                    GetServiceTime( simulationTable, Servers, CustomerNum , ServerNum);
                    simulationTable[CustomerNum - 1].StartTime = Servers[ServerNum].FinishTime;
                    simulationTable[CustomerNum - 1].EndTime = simulationTable[CustomerNum - 1].StartTime + simulationTable[CustomerNum - 1].ServiceTime;
                    simulationTable[CustomerNum - 1].AssignedServer.FinishTime = simulationTable[CustomerNum - 1].EndTime;
                    Servers[ServerNum].FinishTime = simulationTable[CustomerNum - 1].EndTime;



                }

                
            }
        }

        public void Random(int CustomerNum, List<SimulationCase> simulationTable, List<Server> Servers)
        {
            bool Full = false;
            int ServerFull = 0;
            for(int i = 0; i<Servers.Count; i++)
            {
                if(simulationTable[CustomerNum - 1].ArrivalTime < Servers[i].FinishTime)
                {
                    ServerFull++;

                }
            }
            if(ServerFull == Servers.Count)
            {
                Full = true;
            }

           Random rnd = new Random();
            if (CustomerNum == 1)
            {
                int x = rnd.Next(0, Servers.Count);
                GetServiceTime(simulationTable, Servers, 1, x);
                simulationTable[0].StartTime = 0;
                simulationTable[0].EndTime = simulationTable[0].ServiceTime;
                simulationTable[0].AssignedServer.FinishTime = simulationTable[0].ServiceTime;
                Servers[x].FinishTime = simulationTable[CustomerNum - 1].AssignedServer.FinishTime;

            }
            else if (Full)
            {
                int ServerNum = 0;
                int min = 1000;
                List<int> emptyindex = new List<int>();

                for (int i = 0; i < Servers.Count; i++)
                {
                    if (Servers[i].FinishTime < min)
                    {
                        min = Servers[i].FinishTime;
                        ServerNum = i;
                    }
                }
                for (int i = 0; i < Servers.Count; i++)
                {
                    if (Servers[i].FinishTime == min)
                    {
                        emptyindex.Add(i);
                    }
                }
                int Servernum = rnd.Next(0, emptyindex.Count);
                simulationTable[CustomerNum - 1].TimeInQueue = Servers[emptyindex[Servernum]].FinishTime - simulationTable[CustomerNum - 1].ArrivalTime;
                GetServiceTime(simulationTable, Servers, CustomerNum, emptyindex[Servernum]);
                simulationTable[CustomerNum - 1].StartTime = Servers[emptyindex[Servernum]].FinishTime;
                simulationTable[CustomerNum - 1].EndTime = simulationTable[CustomerNum - 1].StartTime + simulationTable[CustomerNum - 1].ServiceTime;
                simulationTable[CustomerNum - 1].AssignedServer.FinishTime = simulationTable[CustomerNum - 1].EndTime;
                Servers[emptyindex[Servernum]].FinishTime = simulationTable[CustomerNum - 1].EndTime;

            }

            else
            {
                { 

                List<int> emptyindexs = new List<int>();

                for (int i = 0; i < Servers.Count; i++)
                {
                    if (simulationTable[CustomerNum - 1].ArrivalTime >= Servers[i].FinishTime)
                    {
                        emptyindexs.Add(i);
                    }
                }
                int y = rnd.Next(0, emptyindexs.Count);

                simulationTable[CustomerNum - 1].AssignedServer.ID = Servers[emptyindexs[y]].ID;
                simulationTable[CustomerNum - 1].AssignedServer.TimeDistribution = Servers[emptyindexs[y]].TimeDistribution;
                simulationTable[CustomerNum - 1].StartTime = simulationTable[CustomerNum - 1].ArrivalTime;
                GetServiceTime(simulationTable, Servers, 1, emptyindexs[y]);
                simulationTable[CustomerNum - 1].StartTime = simulationTable[CustomerNum - 1].ArrivalTime;
                simulationTable[CustomerNum - 1].EndTime = simulationTable[CustomerNum - 1].StartTime + simulationTable[CustomerNum - 1].ServiceTime;
                simulationTable[CustomerNum - 1].AssignedServer.FinishTime = simulationTable[CustomerNum - 1].EndTime;
                Servers[emptyindexs[y]].FinishTime = simulationTable[CustomerNum - 1].AssignedServer.FinishTime;
             
                }
            }

          
        }

        public void leastUtilization(int CustomerNum, List<SimulationCase> simulationTable, List<Server> Servers )
        {
            bool full = true;
            List<int> emptyServerindex = new List<int>();
            if(CustomerNum == 1)
            {
                GetServiceTime(simulationTable, Servers, 1, 0);
                simulationTable[0].StartTime = 0;
                simulationTable[0].EndTime = simulationTable[0].ServiceTime;
                simulationTable[0].AssignedServer.FinishTime = simulationTable[0].ServiceTime;
                Servers[0].FinishTime = simulationTable[CustomerNum - 1].AssignedServer.FinishTime;
                decimal ServiceTime = ServerWorkTime(simulationTable, simulationTable.Count, 1);
                if ((decimal)simulationTable[simulationTable.Count - 1].EndTime != 0)
                {
                    Servers[0].Utilization = ServiceTime / (decimal)simulationTable[simulationTable.Count - 1].EndTime;
                }
                else
                {
                    Servers[0].Utilization = 0;
                }
            }
            else
            {
                for (int i = 0; i < Servers.Count; i++)
                {
                    if (simulationTable[CustomerNum - 1].ArrivalTime >= Servers[i].FinishTime)
                    {
                        emptyServerindex.Add(i);
                    }
                }
                if (emptyServerindex.Count != 0)
                {
                    decimal minimum = 10000;
                    int index=0;
                    for(int i = 0; i< emptyServerindex.Count;i++)
                    {
                        if(Servers[emptyServerindex[i]].Utilization < minimum)
                        {
                            minimum = Servers[emptyServerindex[i]].Utilization;
                            index = i;
                        }
                    }
                    simulationTable[CustomerNum - 1].AssignedServer.ID = Servers[index].ID;
                    simulationTable[CustomerNum - 1].AssignedServer.TimeDistribution = Servers[index].TimeDistribution;
                    simulationTable[CustomerNum - 1].StartTime = simulationTable[CustomerNum - 1].ArrivalTime;
                    GetServiceTime(simulationTable, Servers, CustomerNum, index);
                    simulationTable[CustomerNum - 1].EndTime = simulationTable[CustomerNum - 1].StartTime + simulationTable[CustomerNum - 1].ServiceTime;
                    simulationTable[CustomerNum - 1].AssignedServer.FinishTime = simulationTable[CustomerNum - 1].EndTime;
                    Servers[index].FinishTime = simulationTable[CustomerNum - 1].AssignedServer.FinishTime;
                    decimal ServiceTime = ServerWorkTime(simulationTable, simulationTable.Count , index + 1);
                    Servers[index].Utilization = ServiceTime / (decimal)simulationTable[simulationTable.Count- 1].EndTime;

                }
                else
                {
                    int ServerNum = 0;
                    int min = 1000;
                    List<int> emptyServerind = new List<int>();
                    for (int i = 0; i < Servers.Count; i++)
                    {
                        if (Servers[i].FinishTime < min)
                        {
                            min = Servers[i].FinishTime;
                            ServerNum = i;
                        }
                    }
                    for(int i = 0; i<Servers.Count;i++)
                    {
                        if(Servers[i].FinishTime == min)
                        {
                            emptyServerind.Add(i);
                        }
                    }
                    decimal minimum = 10000;
                    int index = 0;
                    for (int i = 0; i < emptyServerindex.Count; i++)
                    {
                        if (Servers[emptyServerindex[i]].Utilization < minimum)
                        {
                            minimum = Servers[emptyServerindex[i]].Utilization;
                            index = i;
                        }
                    }
                    simulationTable[CustomerNum - 1].TimeInQueue = Servers[index].FinishTime - simulationTable[CustomerNum - 1].ArrivalTime;
                    GetServiceTime(simulationTable, Servers, CustomerNum, index);
                    simulationTable[CustomerNum - 1].StartTime = Servers[index].FinishTime;
                    simulationTable[CustomerNum - 1].EndTime = simulationTable[CustomerNum - 1].StartTime + simulationTable[CustomerNum - 1].ServiceTime;
                    simulationTable[CustomerNum - 1].AssignedServer.FinishTime = simulationTable[CustomerNum - 1].EndTime;
                    Servers[index].FinishTime = simulationTable[CustomerNum - 1].EndTime;
                    decimal ServiceTime = ServerWorkTime(simulationTable, simulationTable.Count, index + 1);
                    if ((decimal)simulationTable[simulationTable.Count - 1].EndTime !=0)
                    {
                        Servers[index].Utilization = ServiceTime / (decimal)simulationTable[simulationTable.Count - 1].EndTime;
                    }
                    else
                    {
                        Servers[index].Utilization = 0;
                    }

                }
                    
                
            }
        }
        public int CalculatingTotalQueueTime(List<SimulationCase> simulationTable , int NumberOfCustomers)
        {
            int totalQueue = 0;
            for(int i = 0 ; i < NumberOfCustomers; i++)
            {
                totalQueue += simulationTable[i].TimeInQueue;
            }
            return totalQueue;
        }


        public int NumberOfCustomersWaited(List<SimulationCase> simulationTable, int NumberOfCustomers)
        {
            int Num = 0;
            for (int i = 0; i < NumberOfCustomers; i++)
            {
                if(simulationTable[i].TimeInQueue != 0)
                {
                    Num++;
                }
                
            }
            return Num;
        }

        public int MaxQueue(List<SimulationCase> simulationTable, int NumberOfCustomers)
        {
            List<int> Max = new List<int>();
            List<int> QueueList = new List<int>();
            for(int i = 0; i<NumberOfCustomers; i++)
            {
                if(simulationTable[i].TimeInQueue !=0)
                {
                    QueueList.Add(i);
                }
            }

            if (QueueList.Count != 0)
            {
                for (int i = 0; i < QueueList.Count; i++)
                {
                    
                    Max.Add(0);
                    for (int j = 0; j < QueueList.Count; j++)
                    {
                        if ((simulationTable[QueueList[i]].ArrivalTime <= simulationTable[QueueList[j]].ArrivalTime) && (simulationTable[QueueList[i]].StartTime) >= (simulationTable[QueueList[j]].ArrivalTime))
                        {
                            Max[i]++;
                        }
                    }

                }

                return Max.Max();
            }
            else
            {
                return 0;
            }
        }


        public void Performance( ref PerformanceMeasures performance , decimal TotalQueue , decimal TotalCustomersQueue , int MaxQueue , decimal NumofCust)
        {
            performance.AverageWaitingTime = TotalQueue / NumofCust;
            performance.MaxQueueLength = MaxQueue;
            performance.WaitingProbability = TotalCustomersQueue / NumofCust;

        }
        

        public decimal TotalNumberOfCustomers(List<SimulationCase> simulationTable, int NumberOfCustomers, int ServerID)
        {
            List<int> Worked = new List<int>();
            for (int i = 0; i < NumberOfCustomers; i++)
            {
                if (simulationTable[i].AssignedServer.ID == ServerID)
                {
                    Worked.Add(i);
                }
            }

            return Worked.Count;
        }
        public decimal ServerWorkTime(List<SimulationCase> simulationTable, int NumberOfCustomers , int ServerID)
        {
            List<int> Worked = new List<int>();
            decimal TotalWorkTime = 0;
            for(int i = 0; i < NumberOfCustomers; i++)
            {
                if(simulationTable[i].AssignedServer.ID == ServerID)
                {
                    Worked.Add(i);
                }
            }

            for(int i = 0; i< Worked.Count; i++)
            {
                TotalWorkTime += simulationTable[Worked[i]].ServiceTime;
            }
            return TotalWorkTime;
        }

        public decimal ServerIdleTime(List<SimulationCase> simulationTable, int NumberOfCustomers, int ServerID , List<Server> Servers)
        {
            decimal Work = ServerWorkTime(simulationTable, NumberOfCustomers, ServerID);
            int Finishtime = simulationTable[NumberOfCustomers - 1].EndTime;
            decimal IdleTime = Finishtime - Work;
            return IdleTime;
            
        }


        public void WorkingTime(List<SimulationCase> simulationTable , List<Server> Servers , int ServerID)
        {
            List<int> Worked = new List<int>();
            int NumberOfCustomers = simulationTable.Count;
            for (int i = 0; i < NumberOfCustomers; i++)
            {

                
                if (simulationTable[i].AssignedServer.ID == ServerID)
                {
                    Worked.Add(i);
                }
            }

            for(int i = 0; i < simulationTable[(simulationTable.Count)-1].EndTime+5 ; i++)
            {
                Servers[ServerID-1].X.Add(i);
                Servers[ServerID-1].y.Add(0);
            }

            for(int i = 0; i < Worked.Count ;i++)
            {
                for(int j = simulationTable[Worked[i]].StartTime ; j < simulationTable[Worked[i]].EndTime ; j++ )
                {
                    Servers[ServerID - 1].y[j] = 1;
                }
            }
            
            
        }

        public void AllSystem( ref SimulationSystem system , ref PerformanceMeasures PerformanceMeasures)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\abdal\OneDrive\Desktop\Assignment1\MultiQueueSimulation\TestCases\TestCase1.txt");
            int ServerNumbers = int.Parse(lines[1]);

            bool server = false, stpNum = false, stpC = false, slctMethod = false, ID = false;
            int stoppingNumber = 0, ServerID = 0, stoppingCriteria = 0, selectMethod = 0;
            List<int> IDTime = new List<int>();
            List<decimal> IDProbability = new List<decimal>();

            List<int>[] ServerTime = new List<int>[50];
            List<decimal>[] ServerProb = new List<decimal>[50];
            for (int i = 0; i < 50; i++)
            {
                ServerTime[i] = new List<int>();
                ServerProb[i] = new List<decimal>();
            }
            foreach (string line in lines)
            {
                if (line.Length <= 0)
                {
                    continue;
                }
                if (line == "StoppingNumber")
                {
                    stpNum = true;
                    continue;
                }
                else if (line == "StoppingCriteria")
                {
                    stpC = true;
                    stpNum = false;
                    continue;
                }
                else if (line == "SelectionMethod")
                {
                    slctMethod = true; stpC = false; stpNum = false;
                    continue;
                }
                else if (line == "InterarrivalDistribution")
                {
                    slctMethod = false; stpC = false; stpNum = false; ID = true;
                    continue;
                }
                else if (line.Length >= 7 && line.Substring(0, 7) == "Service")
                {
                    ServerID = line[line.Length - 1] - '0';
                    server = true;
                    slctMethod = false; stpC = false; stpNum = false; ID = false;
                    continue;
                }

                if (stpNum) stoppingNumber = int.Parse(line);
                else if (stpC) stoppingCriteria = int.Parse(line);
                else if (slctMethod) selectMethod = int.Parse(line);
                else if (ID)
                {
                    string time = "", prob = "";
                    bool comma = false;
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (line[i] == ',') comma = true;
                        else if (!comma) time += line[i];
                        else prob += line[i];
                    }
                    int x = int.Parse(time);
                    decimal y = decimal.Parse(prob);
                    IDTime.Add(x);
                    IDProbability.Add(y);
                }
                else if (server)
                {
                    string time = "", prob = "";
                    bool comma = false;
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (line[i] == ',') comma = true;
                        else if (!comma) time += line[i];
                        else prob += line[i];
                    }
                    int x = int.Parse(time);
                    decimal y = decimal.Parse(prob);
                    ServerTime[ServerID - 1].Add(x);
                    ServerProb[ServerID - 1].Add(y);
                }
            }
            int TotalQueue;
            int TotalCustomersQueue;
            int MaxTimeinQueue;
            Enums num = new Enums();
            List<SimulationCase> simulationTable = new List<SimulationCase>();
            system.SimulationTable = simulationTable;
            List<Server> Servers = new List<Server>();
            for (int i = 0; i < ServerNumbers; i++)
            {
                Server Server = new Server();
                Servers.Add(Server);
            }
            system.NumberOfServers = ServerNumbers;
            system.StoppingNumber = stoppingNumber;
            system.StoppingCriteria = Enums.StoppingCriteria.NumberOfCustomers;
            if (selectMethod == 1)
            {
                system.SelectionMethod = Enums.SelectionMethod.HighestPriority;

            }
            else if (selectMethod == 2)
            {
                system.SelectionMethod = Enums.SelectionMethod.Random;

            }
            for (int i = 0; i < system.StoppingNumber; i++)
            {
                SimulationCase Simulation = new SimulationCase();
                simulationTable.Add(Simulation);
            }

            List<TimeDistribution> interarrival = new List<TimeDistribution>();
            for (int i = 0; i < IDTime.Count; i++)
            {
                TimeDistribution AT1 = new TimeDistribution();
                interarrival.Add(AT1);
                interarrival[i].Time = IDTime[i];
                interarrival[i].Probability = IDProbability[i];
            }


            for (int i = 0; i < ServerNumbers; i++)
            {
                List<TimeDistribution> S1 = new List<TimeDistribution>();
                for (int j = 0; j < ServerTime[i].Count; j++)
                {
                    TimeDistribution ST1 = new TimeDistribution();
                    S1.Add(ST1);
                    S1[j].Time = ServerTime[i][j];
                    S1[j].Probability = ServerProb[i][j];
                }
                Servers[i].TimeDistribution = S1;
            }



            system.Servers = Servers;
            system.CalculatingCummProb(interarrival);
            system.CalculatingMinMaxRange(interarrival);
            system.InterarrivalDistribution = interarrival;
            system.GeneratingRandomNumber(simulationTable, system.StoppingNumber);
            system.TimeBetweenArrivals(simulationTable, interarrival, system.StoppingNumber);
            system.TimeOfArrivals(simulationTable, system.StoppingNumber);
            system.SettingServersID(Servers);
            system.SetServiceTimeDistribution(Servers);

            if (selectMethod == 1)
            {
                for (int i = 1; i <= system.StoppingNumber; i++)
                {
                    system.HighestPriority(i, simulationTable, Servers);
                }
            }
            else if (selectMethod == 2)
            {
                for (int i = 1; i <= system.StoppingNumber; i++)
                {
                    system.Random(i, simulationTable, Servers);
                }
            }
            else if (selectMethod == 3)
            {
                for (int i = 1; i <= system.StoppingNumber; i++)
                {
                    system.leastUtilization(i, simulationTable, Servers);
                }
            }

            TotalQueue = system.CalculatingTotalQueueTime(simulationTable, system.StoppingNumber);
            TotalCustomersQueue = system.NumberOfCustomersWaited(simulationTable, system.StoppingNumber);
            MaxTimeinQueue = system.MaxQueue(simulationTable, system.StoppingNumber);

            system.Performance(ref PerformanceMeasures, TotalQueue, TotalCustomersQueue, MaxTimeinQueue, system.StoppingNumber);
            system.PerformanceMeasures = PerformanceMeasures;
            for (int i = 0; i < system.NumberOfServers; i++)
            {
                decimal idle = system.ServerIdleTime(simulationTable, system.StoppingNumber, i + 1, Servers);
                system.Servers[i].IdleProbability = idle / (decimal)simulationTable[system.StoppingNumber - 1].EndTime;

                decimal ServiceTime = system.ServerWorkTime(simulationTable, system.StoppingNumber, i + 1);
                if ((decimal)system.TotalNumberOfCustomers(simulationTable, system.StoppingNumber, i + 1) != 0)
                {
                    system.Servers[i].AverageServiceTime = ServiceTime / (decimal)system.TotalNumberOfCustomers(simulationTable, system.StoppingNumber, i + 1);
                }
                else
                {
                    system.Servers[i].AverageServiceTime = 0;
                }
                system.Servers[i].Utilization = ServiceTime / (decimal)simulationTable[system.StoppingNumber - 1].EndTime;
            }

            for (int i = 0; i < Servers.Count; i++)
            {
                system.WorkingTime(simulationTable, Servers, i + 1);
            }
        }

        

        
      
    }
    
}
