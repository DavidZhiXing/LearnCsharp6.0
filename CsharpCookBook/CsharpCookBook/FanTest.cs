using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CsharpCookBook
{
    public class FanTest
    {

        public async void FanTest2()
        {
            List<Fan> fansAttending = new List<Fan>();
            for (int i = 0; i < 100; i++)
            {
                fansAttending.Add(new Fan() { Name = "Fan" + i });

            }
            Fan[] fans = fansAttending.ToArray();
            int gateCount = 10;
            var entryGates = new Task[gateCount];
            var securityMonitors = new Task[gateCount];
            for (int i = 0; i < gateCount; i++)
            {
                int GateNum = i;
                Action action = delegate () { AdmitFans(fans, i, gateCount); };
                entryGates[i] = Task.Run(action);
            }
            for (int gateNumber = 0; gateNumber < gateCount; gateNumber++)
            {
                int GateNum = gateNumber;
                Action action = delegate () { MonitorGate(GateNum); };
                securityMonitors[gateNumber] = Task.Run(action);

            }
            await Task.WhenAll(entryGates);
            monitorGates = false;

        }

        private static void AdmitFans(Fan[] fans, int i, int gateCount)
        {
            Random rnd = new Random();
            int fansPerGate = fans.Length / gateCount;
            int start = i * fansPerGate;
            int end = start + fansPerGate - 1;
            for (int f = start; f <= end; f++)
            {
                Console.WriteLine($"Admitting{fans[f].Name} through gate {i}");
                var fanAtGate =
                    stadiumGates.AddOrUpdate(i, fans[f],
                    (key, fanInGate) =>
                    {
                        Console.WriteLine($"{fanInGate.Name} was replaced by" +
                            $"{fans[f].Name} in gate {i}");
                        return fans[f];
                    });
                Thread.Sleep(rnd.Next(500, 2000));
                fans[f].Admintted = DateTime.Now;
                fans[f].AdmittanceGateNumber = i;
                Fan fanAdmitted;
                if (stadiumGates.TryRemove(i, out fanAdmitted))
                    Console.WriteLine($"{fanAdmitted.Name} entering event from gate" +
                        $"{fanAdmitted.AdmittanceGateNumber} on" +
                        $"{fanAdmitted.Admintted.ToShortTimeString()}");
                else
                {
                    Console.WriteLine($"{fanAdmitted.Name} held by security" +
                        $"{fanAdmitted.AdmittanceGateNumber}");
                }
            }
        }

        private static void MonitorGate(int gateNumber)
        {
            Random rnd = new Random();
            while (monitorGates)
            {
                Fan currentFanInGate;
                if (stadiumGates.TryGetValue(gateNumber, out currentFanInGate))
                {
                    Console.WriteLine($"Monitor:{currentFanInGate.Name} is in Gate " +
                        $"{gateNumber}");

                }
                else
                    Console.WriteLine($"No fan is in Gate {gateNumber}");
            }
            Thread.Sleep(rnd.Next(500, 2000));
        }
        private static ConcurrentDictionary<int, Fan> stadiumGates =
                new ConcurrentDictionary<int, Fan>();
        private static bool monitorGates = true;
    }
}
