using MangaCrawler.Crawler.Database;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MangaCrawler.Crawler.Data;

namespace MangaCrawler.Crawler.Job
{
    static class JobScheduler
    {
        static Queue<JobDescription> queueJob = new Queue<JobDescription>();
        static Thread[] threads = new Thread[4];
        static Timer timer = new Timer(new TimerCallback(Work), null, Timeout.Infinite, 1000);

        public static void Start()
        {
            timer.Change(0, 1000);
        }

        private static void PushJob(JobDescription job)
        {
            queueJob.Enqueue(job);
        }

        private static void Work(object state)
        {
            CollectWork();

            for (int i = 0; i < threads.Length; i++)
            {
                var thread = threads[i];

                if ((
                    thread == null ||
                    thread.ThreadState == ThreadState.Stopped ||
                    thread.ThreadState == ThreadState.Aborted) && queueJob.Count > 0)
                {
                    var job = queueJob.Dequeue();
                    var thStart = new ThreadStart(job.Download);

                    thread = new Thread(thStart);
                    thread.Start();
                }
            }
        }

        private static void CollectWork()
        {
            using (var conn = Connector.GetConnection())
            {
                var sqlm = "SELECT * FROM manga WHERE Thumb IS NULL AND ThumbUrl IS NOT NULL";
                var sqlc = "SELECT * FROM chapter WHERE Thumb IS NULL AND ThumbUrl IS NOT NULL";
                var resm = conn.Query<Manga>(sqlm);
                var resc = conn.Query<Chapter>(sqlc);

                foreach (var data in resm)
                {
                    var job = new JobDescription()
                    {
                        UrlDownload = data.ThumbUrl,
                        Entity = data
                    };

                    PushJob(job);
                }

                foreach (var data in resc)
                {
                    var job = new JobDescription()
                    {
                        UrlDownload = data.ThumbUrl,
                        Entity = data
                    };

                    PushJob(job);
                }
            }
        }
    }
}
