using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceProcess;

namespace ZifliAdmin
{
    class SvcController
    {
        public ServiceController[] services;

        public int stopSvc()
        {
            services = ServiceController.GetServices();
            foreach (ServiceController service in services)
            {
                if (service.ServiceName == "ZifliService")
                {
                    try
                    {
                        service.Stop();
                        service.WaitForStatus(ServiceControllerStatus.Stopped);
                        return 0;
                    }
                    catch
                    {
                        AdminLogger.WriteEvent("Failed to stop service.",
                            ZifliForm._ERR);
                        return 1;
                    }
                }
            }
            return 2;
        }

        public int startSvc()
        {
            services = ServiceController.GetServices();
            foreach (ServiceController service in services)
            {
                if (service.ServiceName == "ZifliService")
                {
                    try
                    {
                        service.Start();
                        service.WaitForStatus(ServiceControllerStatus.Running);
                        return 0;
                    }
                    catch
                    {
                        AdminLogger.WriteEvent("Failed to start service.",
                            ZifliForm._ERR);
                        return 1;
                    }
                }
            }
            return 2;
        }

        public int isSvcRunning()
        {
            services = ServiceController.GetServices();
            foreach (ServiceController service in services)
            {
                if (service.ServiceName == "ZifliService")
                {
                    if (service.Status.ToString() == "Running")
                    {
                        AdminLogger.WriteEvent("Service is running.",
                            ZifliForm._DEBUG);
                        return 0;
                    }
                    else
                    {
                        AdminLogger.WriteEvent("Service must be stopped.",
                            ZifliForm._DEBUG);
                        return 1;
                    }
                }
                else
                {
                    //AdminLogger.WriteEvent("Not the service we're looking for.",
                    //    ZifliForm._INFO);
                }
            }
            AdminLogger.WriteEvent("Service is not installed.",
                    ZifliForm._ERR);
            return 2;
        }
    }
}
