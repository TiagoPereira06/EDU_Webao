using DynWebao.DynBuilder;
using DynWebao.IWebaos;
using Webao;
using Webao.Test.LastFmWebaos;
using Webao.Test.MockRequests;
using Webao.Test.TvMazeWebaos;

namespace WebaoBench
{
    public class WebaoBench
    {
        private static readonly IWebaoDynArtist dynArtistWebao =
            (IWebaoDynArtist) WebaoDynBuilder
                .Build(typeof(IWebaoDynArtist), new LastfmMockRequest());

        private static readonly IWebaoDynTrack dynTrackWebao =
            (IWebaoDynTrack) WebaoDynBuilder
                .Build(typeof(IWebaoDynTrack), new LastfmMockRequest());

        private static readonly IWebaoDynEpisode dynEpisodeWebao =
            (IWebaoDynEpisode) WebaoDynBuilder
                .Build(typeof(IWebaoDynEpisode), new TvMazeMockRequest());

        private static readonly WebaoArtist artistWebao =
            (WebaoArtist) WebaoBuilder.Build(typeof(WebaoArtist), new LastfmMockRequest());

        private static readonly WebaoTrack trackWebao =
            (WebaoTrack) WebaoBuilder.Build(typeof(WebaoTrack), new LastfmMockRequest());

        private static readonly WebaoEpisode episodeWebao =
            (WebaoEpisode) WebaoBuilder.Build(typeof(WebaoEpisode), new TvMazeMockRequest());

        public static object NoOperation()
        {
            return null;
        }

        public static object ArtistDynTest()
        {
            return GetWebao.GetDynArtistInfo(dynArtistWebao);
        }

        public static object ArtistTest()
        {
            return GetWebao.GetArtistInfo(artistWebao);
        }


        public static object TrackDynTest()
        {
            return GetWebao.GetDynTrackTop(dynTrackWebao);
        }

        public static object TrackTest()
        {
            return GetWebao.GetTrackTop(trackWebao);
        }


        public static object EpisodeDynTest()
        {
            return GetWebao.GetDynEpisodeInfo(dynEpisodeWebao);
        }

        public static object EpisodeTest()
        {
            return GetWebao.GetEpisodeInfo(episodeWebao);
        }

        public static void Main(string[] args)
        {
            const long ITER_TIME = 1000;
            const long NUM_WARMUP = 10;
            const long NUM_ITER = 10;

            NBench.Benchmark(NoOperation, "noOperation", ITER_TIME, NUM_WARMUP, NUM_ITER);

            NBench.Benchmark(ArtistDynTest, "artistDynTest", ITER_TIME, NUM_WARMUP, NUM_ITER);
            NBench.Benchmark(ArtistTest, "artistTest", ITER_TIME, NUM_WARMUP, NUM_ITER);

            NBench.Benchmark(TrackDynTest, "TrackDynTest", ITER_TIME, NUM_WARMUP, NUM_ITER);
            NBench.Benchmark(TrackTest, "TrackTest", ITER_TIME, NUM_WARMUP, NUM_ITER);

            NBench.Benchmark(EpisodeDynTest, "EpisodeDynTest", ITER_TIME, NUM_WARMUP, NUM_ITER);
            NBench.Benchmark(EpisodeTest, "EpisodeTest", ITER_TIME, NUM_WARMUP, NUM_ITER);
        }
    }
}

// Executado em modo release, sem IO
// SIM #1 - Standard
/*
:: BENCHMARKING noOperation ::
# Warmup Iteration  1: 411565376 ops/s      2,43 ns
# Warmup Iteration  2: 417330144 ops/s      2,40 ns
# Warmup Iteration  3: 414803232 ops/s      2,41 ns
# Warmup Iteration  4: 415027264 ops/s      2,41 ns
# Warmup Iteration  5: 417516640 ops/s      2,40 ns
# Warmup Iteration  6: 405163072 ops/s      2,47 ns
# Warmup Iteration  7: 418295104 ops/s      2,39 ns
# Warmup Iteration  8: 417654880 ops/s      2,39 ns
# Warmup Iteration  9: 416957280 ops/s      2,40 ns
# Warmup Iteration 10: 418124864 ops/s      2,39 ns
Iteration  1: 412621088 ops/s       2,42 ns
Iteration  2: 417405952 ops/s       2,40 ns
Iteration  3: 417293280 ops/s       2,40 ns
Iteration  4: 418047456 ops/s       2,39 ns
Iteration  5: 418448288 ops/s       2,39 ns
Iteration  6: 418985312 ops/s       2,39 ns
Iteration  7: 417276832 ops/s       2,40 ns
Iteration  8: 417558816 ops/s       2,39 ns
Iteration  9: 417869888 ops/s       2,39 ns
Iteration 10: 419040832 ops/s       2,39 ns

Result: 419040832 ops/s     2,39 ns


:: BENCHMARKING artistDynTest ::
# Warmup Iteration  1: 2903072 ops/s      344,46 ns
# Warmup Iteration  2: 3057856 ops/s      327,03 ns
# Warmup Iteration  3: 3023328 ops/s      330,76 ns
# Warmup Iteration  4: 3056256 ops/s      327,20 ns
# Warmup Iteration  5: 3047488 ops/s      328,14 ns
# Warmup Iteration  6: 3053248 ops/s      327,52 ns
# Warmup Iteration  7: 3014496 ops/s      331,73 ns
# Warmup Iteration  8: 3057920 ops/s      327,02 ns
# Warmup Iteration  9: 2972608 ops/s      336,40 ns
# Warmup Iteration 10: 3028448 ops/s      330,20 ns
Iteration  1: 3030624 ops/s       329,97 ns
Iteration  2: 3031488 ops/s       329,87 ns
Iteration  3: 3060384 ops/s       326,76 ns
Iteration  4: 3035328 ops/s       329,45 ns
Iteration  5: 3043488 ops/s       328,57 ns
Iteration  6: 3043936 ops/s       328,52 ns
Iteration  7: 2994048 ops/s       334,00 ns
Iteration  8: 2991584 ops/s       334,27 ns
Iteration  9: 3048640 ops/s       328,02 ns
Iteration 10: 3025920 ops/s       330,48 ns

Result: 3060384 ops/s     326,76 ns


:: BENCHMARKING artistTest ::
# Warmup Iteration  1: 42272 ops/s      23656,32 ns
# Warmup Iteration  2: 43072 ops/s      23216,94 ns
# Warmup Iteration  3: 43072 ops/s      23216,94 ns
# Warmup Iteration  4: 43008 ops/s      23251,49 ns
# Warmup Iteration  5: 43040 ops/s      23234,20 ns
# Warmup Iteration  6: 42272 ops/s      23656,32 ns
# Warmup Iteration  7: 40864 ops/s      24471,42 ns
# Warmup Iteration  8: 40416 ops/s      24742,68 ns
# Warmup Iteration  9: 40544 ops/s      24664,56 ns
# Warmup Iteration 10: 39552 ops/s      25283,17 ns
Iteration  1: 40160 ops/s       24900,40 ns
Iteration  2: 42752 ops/s       23390,72 ns
Iteration  3: 42624 ops/s       23460,96 ns
Iteration  4: 42080 ops/s       23764,26 ns
Iteration  5: 42624 ops/s       23460,96 ns
Iteration  6: 42432 ops/s       23567,12 ns
Iteration  7: 43168 ops/s       23165,31 ns
Iteration  8: 42656 ops/s       23443,36 ns
Iteration  9: 42304 ops/s       23638,43 ns
Iteration 10: 42784 ops/s       23373,22 ns

Result: 43168 ops/s     23165,31 ns


:: BENCHMARKING TrackDynTest ::
# Warmup Iteration  1: 2600160 ops/s      384,59 ns
# Warmup Iteration  2: 2615136 ops/s      382,39 ns
# Warmup Iteration  3: 2621984 ops/s      381,39 ns
# Warmup Iteration  4: 2594624 ops/s      385,41 ns
# Warmup Iteration  5: 2627360 ops/s      380,61 ns
# Warmup Iteration  6: 2574560 ops/s      388,42 ns
# Warmup Iteration  7: 2601088 ops/s      384,45 ns
# Warmup Iteration  8: 2649632 ops/s      377,41 ns
# Warmup Iteration  9: 2650272 ops/s      377,32 ns
# Warmup Iteration 10: 2573184 ops/s      388,62 ns
Iteration  1: 2632896 ops/s       379,81 ns
Iteration  2: 2642912 ops/s       378,37 ns
Iteration  3: 2644416 ops/s       378,16 ns
Iteration  4: 2619392 ops/s       381,77 ns
Iteration  5: 2639168 ops/s       378,91 ns
Iteration  6: 2618784 ops/s       381,86 ns
Iteration  7: 2585216 ops/s       386,81 ns
Iteration  8: 2607008 ops/s       383,58 ns
Iteration  9: 2635328 ops/s       379,46 ns
Iteration 10: 2645696 ops/s       377,97 ns

Result: 2645696 ops/s     377,97 ns


:: BENCHMARKING TrackTest ::
# Warmup Iteration  1: 40800 ops/s      24509,80 ns
# Warmup Iteration  2: 41376 ops/s      24168,60 ns
# Warmup Iteration  3: 41376 ops/s      24168,60 ns
# Warmup Iteration  4: 41568 ops/s      24056,97 ns
# Warmup Iteration  5: 41728 ops/s      23964,72 ns
# Warmup Iteration  6: 41664 ops/s      24001,54 ns
# Warmup Iteration  7: 41760 ops/s      23946,36 ns
# Warmup Iteration  8: 41600 ops/s      24038,46 ns
# Warmup Iteration  9: 41312 ops/s      24206,04 ns
# Warmup Iteration 10: 41120 ops/s      24319,07 ns
Iteration  1: 41216 ops/s       24262,42 ns
Iteration  2: 41440 ops/s       24131,27 ns
Iteration  3: 41248 ops/s       24243,60 ns
Iteration  4: 41888 ops/s       23873,19 ns
Iteration  5: 41472 ops/s       24112,65 ns
Iteration  6: 41056 ops/s       24356,98 ns
Iteration  7: 41344 ops/s       24187,31 ns
Iteration  8: 41888 ops/s       23873,19 ns
Iteration  9: 41440 ops/s       24131,27 ns
Iteration 10: 41632 ops/s       24019,98 ns

Result: 41888 ops/s     23873,19 ns


:: BENCHMARKING EpisodeDynTest ::
# Warmup Iteration  1: 766112 ops/s      1305,29 ns
# Warmup Iteration  2: 771808 ops/s      1295,66 ns
# Warmup Iteration  3: 772256 ops/s      1294,91 ns
# Warmup Iteration  4: 751392 ops/s      1330,86 ns
# Warmup Iteration  5: 766848 ops/s      1304,04 ns
# Warmup Iteration  6: 773152 ops/s      1293,41 ns
# Warmup Iteration  7: 774880 ops/s      1290,52 ns
# Warmup Iteration  8: 768384 ops/s      1301,43 ns
# Warmup Iteration  9: 761920 ops/s      1312,47 ns
# Warmup Iteration 10: 758528 ops/s      1318,34 ns
Iteration  1: 717216 ops/s       1394,28 ns
Iteration  2: 756032 ops/s       1322,70 ns
Iteration  3: 725856 ops/s       1377,68 ns
Iteration  4: 746528 ops/s       1339,53 ns
Iteration  5: 743968 ops/s       1344,14 ns
Iteration  6: 671200 ops/s       1489,87 ns
Iteration  7: 716448 ops/s       1395,77 ns
Iteration  8: 767904 ops/s       1302,25 ns
Iteration  9: 757600 ops/s       1319,96 ns
Iteration 10: 681664 ops/s       1467,00 ns

Result: 767904 ops/s     1302,25 ns


:: BENCHMARKING EpisodeTest ::
# Warmup Iteration  1: 41280 ops/s      24224,81 ns
# Warmup Iteration  2: 42304 ops/s      23638,43 ns
# Warmup Iteration  3: 42144 ops/s      23728,17 ns
# Warmup Iteration  4: 42784 ops/s      23373,22 ns
# Warmup Iteration  5: 41152 ops/s      24300,16 ns
# Warmup Iteration  6: 42016 ops/s      23800,46 ns
# Warmup Iteration  7: 42272 ops/s      23656,32 ns
# Warmup Iteration  8: 42656 ops/s      23443,36 ns
# Warmup Iteration  9: 41440 ops/s      24131,27 ns
# Warmup Iteration 10: 42336 ops/s      23620,56 ns
Iteration  1: 40416 ops/s       24742,68 ns
Iteration  2: 41344 ops/s       24187,31 ns
Iteration  3: 42496 ops/s       23531,63 ns
Iteration  4: 42432 ops/s       23567,12 ns
Iteration  5: 42720 ops/s       23408,24 ns
Iteration  6: 42912 ops/s       23303,50 ns
Iteration  7: 42624 ops/s       23460,96 ns
Iteration  8: 42080 ops/s       23764,26 ns
Iteration  9: 42272 ops/s       23656,32 ns
Iteration 10: 42912 ops/s       23303,50 ns

Result: 42912 ops/s     23303,50 ns
*/