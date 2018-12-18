using System;
using System.Collections.Generic;
using System.Text;
using Day02;
using Xunit;

namespace Tests.Day02
{
    public class Day2Tests
    {
        IList<string> sampleIds = new List<string> { "abcdef", "bababc", "abbcde", "abcccd", "aabcdd", "abcdee", "ababab" };

        IList<string> actualData = new List<string> { "nkucgflathzwwijtrevymbdplq", "nkucgflathzwsidxrlvymbdpiq", "nkucgjlathqwsijxrevymbypoq", "nkucgflarrzwsmjxrevymbdpoq", "nkucgflzthtwsijxrevymbdpjq", "nkucgflahhzwskjxrevymbgpoq", "bkycgflathzwsijxrsvymbdpoq", "nkucgflathzwsijxdevymbdmog", "nkucgflaehzwsmjxrevymbdpow", "nkucgflathzwsijxrevwmbdnbq", "nkucgflathzssijxrevynbdqoq", "ngucgflathzwsijxsevymndpoq", "nfucgflathzvsijxrevymbspoq", "nkucgflwthzwsijxreeymbdpkq", "nkucgflpthzwsijxrevdmbdpoe", "nkungflatuzwsijurevymbdpoq", "nkucgflathzwsiqxrevyybdpom", "nkucgflathzwsicxrevtmbtpoq", "nkucgfladhzwsijxreuymbdboq", "nkumgflathznsijxzevymbdpoq", "nkuagflethzwsijxrqvymbdpoq", "nkucgflatozwhijxrevymbdpkq", "nkuggflathzwsijxrejymbdpob", "nkucgflathzwlijxrqvambdpoq", "hkucnflathzasijxrevymbdpoq", "nkuigflathzwsirxrevymbdooq", "nkucgflatezwsijxwetymbdpoq", "nkucmflavhbwsijxrevymbdpoq", "nkucgflathzwssjxrevytbmpoq", "nkucgflmthzwsvjxrevymbdpgq", "nkucgtlathzwsijcrevymbjpoq", "nkucgflathfwsfjxrevymbdpsq", "nkucgflathjwsijxrwvymbdpok", "nkucgeldthzwsijxrevymqdpoq", "nkutgcpathzwsijxrevymbdpoq", "nkucgflaehzmsijxrevymydpoq", "mkucdflathzwsvjxrevymbdpoq", "nkucgflathzwsijxtevymidpfq", "nkucgfllthzwsijirevlmbdpoq", "nkucgfuathzwsijxrevymbqpou", "nkufgflathzwsijxrgvymbdpor", "nkuygflatrzwsijxrevymbdpoo", "nkunoflathzwsijxrevyabdpoq", "nksogflathzwsijxrevymbdpeq", "nkucgflathzwciexrevymbdhoq", "nkucgfnathzwsijxdevyobdpoq", "nkudgflazhzwsijxrevymbmpoq", "nkucgylathzwscjxrevymbdaoq", "nkucgflqthzisijxrerymbdpoq", "nkucgxlathzwsijxgebymbdpoq", "nkucgflathzssijxrwvymadpoq", "nkucgflathzwsijxrvvymbdloi", "nkucaflathzwskjxzevymbdpoq", "wkuchflathzwsijxrevymbdioq", "nkucgilathzwsijxrgvympdpoq", "nkubgflaohzwsijxrevymbnpoq", "nkucgwoathzwnijxrevymbdpoq", "nkprgflathzwsijxrevywbdpoq", "nkucgflatlzwsijxievymzdpoq", "nkucgflathzwsijxrevymbmdow", "nkucgzlathzwsitlrevymbdpoq", "nkubgfladhzwsijxrevymbdpsq", "nkucgflathzwsijxrzvyibdroq", "wkucgflathznsijqrevymbdpoq", "nkupgilathzwsijxrnvymbdpoq", "nkucgflathzwwijnrevymgdpoq", "nkucgflathjwsijxrewymbopoq", "mkwcdflathzwsvjxrevymbdpoq", "nkucgflathzwsujxoevymbdboq", "nkucvflathzwsojxrevymzdpoq", "nkocgflabhzwsijxrevyebdpoq", "skuciflpthzwsijxrevymbdpoq", "nkuxgflathzrsijxrevymbupoq", "nkucgblathzwsezxrevymbdpoq", "nkucgflathzwsijxrevymbvtop", "nkucdflathzwsiaxrefymbdpoq", "nkucgflathzwsijxzevkmbdpmq", "nkucgflarhzwsijroevymbdpoq", "nkuccflathzwsinxrevymsdpoq", "nkucgflathzwsijxregymidsoq", "nkucgflathnwsijxrvvumbdpoq", "nkucgfaathzwssjxrevymbdwoq", "nkucgflothzwsijxrevymbdloz", "naucgulathzwsijxremymbdpoq", "nkucgflaqhzwsijxrevymbdnqq", "wkucgflatrzwsijxrevymbdpof", "nvucgflaehzwyijxrevymbdpoq", "nkusaflaahzwsijxrevymbdpoq", "nkucgfkathzwsijxrevymbdbfq", "nkucgfkathzwsijrrevymodpoq", "nkuegflathzwsijxrrvbmbdpoq", "ykucgflathzwsijfrlvymbdpoq", "nkucgflathzrsujxrevymbdkoq", "nkuclflatsowsijxrevymbdpoq", "nkucgflathzwsgjxrqvymbdpor", "ekucgflathzwsijorevlmbdpoq", "nkucgflathizsijgrevymbdpoq", "nkucgfoathzksijbrevymbdpoq", "nkucgflachzwsijxrevymbupoa", "nkuhgflathzwsijxravylbdpoq", "nkncgflithzwsijnrevymbdpoq", "nvucgjlathzjsijxrevymbdpoq", "nhucgflathzwsijxrtvymbtpoq", "akucgflathzwhijxrevymbdpor", "nkucgflatozisijxrvvymbdpoq", "nkucgzlatgzwsijxrevymbepoq", "nkjcgflqthmwsijxrevymbdpoq", "nkucgflaohzosijxrhvymbdpoq", "ntucgflatrzwsijxrevymbdpol", "nkucgflathzwsijkriqymbdpoq", "nkuggflathzwsijmrevymbdpvq", "nkucgflpmhzwsmjxrevymbdpoq", "okucgflathzwsijxrevgmbdsoq", "nkucgflaehzwkijxrevymwdpoq", "zkucgfllthzwsijxrevymbdpod", "nkicgflathzasijxfevymbdpoq", "kkucgfhathzwsijxaevymbdpoq", "nkucqflsthzwsijxrevyjbdpoq", "nkucgflaghzwsijxoevykbdpoq", "nkucgflaohzwsljxryvymbdpoq", "bkucfflathzwsijxrexymbdpoq", "nkucnflathzwsbjxrpvymbdpoq", "nkucjflatlzwsijxrevymqdpoq", "nkucgflathzwsijsyevyxbdpoq", "nkwcgflathzosijxqevymbdpoq", "nkucgslathzesijxievymbdpoq", "nkuciflauhzwsiaxrevymbdpoq", "nkucgflathzwsiwxreeymbdwoq", "nkucgblatwzwsijxkevymbdpoq", "njucgfkathzwsijxrevymbvpoq", "nkucgfladhzwsijfrevyibdpoq", "nkukgflathzwsijprenymbdpoq", "nkucgflathzwsijxrchymbupoq", "nkucgfeathzwsitxaevymbdpoq", "nkufjflathzwsijxresymbdpoq", "nkuggflatlzwsijxrevymbdpoa", "nkucgflsthnwsijxrevumbdpoq", "nkuceflathzwsnjxrevymbmpoq", "nkucgflabhzwsijxrevymblplq", "nkucgfmathzwsijxrevdybdpoq", "niuvgflathzwsijxrcvymbdpoq", "nkscgflathzwsijxrevyzbdooq", "nkucgflatszwsbjxrevymbgpoq", "nkucgflazhzwsijxcevymzdpoq", "nkucgflathzwsfjqrevymbdpxq", "nkucgflathcwsijxrrvymbdroq", "nkurgflathzwsijxrepymzdpoq", "nlucgflathzwrijxrevdmbdpoq", "kkucgflkthzwswjxrevymbdpoq", "nktcgflathzwgijxrevbmbdpoq", "nbucgfiathzwsijxreyymbdpoq", "lkucgflathswsijxrevymbdpxq", "ntucgflathzwswrxrevymbdpoq", "nkscgflathzwssjxravymbdpoq", "nuocgflathzwsijxrevyebdpoq", "nbucgllathzwsijxregymbdpoq", "ckucbflathzwsijxrelymbdpoq", "nkucgflathzwsijxremymbqpor", "nkgcgfljthzwsijkrevymbdpoq", "nkdcgflashzwsijxrjvymbdpoq", "nkecgflathzwsijxuevumbdpoq", "njucgflatpfwsijxrevymbdpoq", "nkucgwlathzjsijxrevymbzpoq", "nkucgfxathzqsijxrenymbdpoq", "dkfcgflathzwsijxrevymbdtoq", "nkupgfhathzwsijxrevymbjpoq", "nkucgflathzwsjjxrevymldooq", "pkucgfbathhwsijxrevymbdpoq", "nkuciflayhzwsijxrevymbdpfq", "nkucpfdathzwsajxrevymbdpoq", "ykucgflathdwsijzrevymbdpoq", "nkucgwlstnzwsijxrevymbdpoq", "nkucwfzazhzwsijxrevymbdpoq", "nkucgflatczwssjxretymbdpoq", "nkucgflathzwsijpreaymxdpoq", "ntucgflathzwsijxrepymvdpoq", "nkucgqlathzdsijxrevymbopoq", "nkucgflathzusijxfevymbdptq", "nkocgflathzwdijxrevymbipoq", "nklcgflatgzwsijxrevymbdsoq", "nkucgflathzwsgjxgevymbopoq", "nkucgflathzwuijxreaymbdyoq", "nkucgwlathzwsvjxrevymbdpos", "nkucrflathzwliqxrevymbdpoq", "nkucgflathzxsijxievysbdpoq", "nkufgolhthzwsijxrevymbdpoq", "niucgflathzwsiixrevyabdpoq", "nkucgflathzhsijxrevymbdyuq", "nkucgqlathzwsijxreaymbdpob", "nzucgflathzesijxrevymwdpoq", "nkucgflatlzwsirxrevymmdpoq", "nkucgfxavhzwsijxrevymbwpoq", "nkucgflathswsijxrevvmbdpoe", "nkucgfgethzwsrjxrevymbdpoq", "nkucgzlayhzwsinxrevymbdpoq", "nkucgflwthzwsiyxrevymbdpdq", "nkucgflpthzwsijxrezombdpoq", "nkurgflathdwsijxuevymbdpoq", "nkjcgflathzwsijxrevkmbdpoc", "nkucmflatuzwsijxrevmmbdpoq", "nkucgfldthzwsijxrevevbdpoq", "nkucgflatrzgsijxrevambdpoq", "nkicgflathzwsijxrevymhdhoq", "nkbcgflathzwsijxrevymxdpos", "nkucgflatfzwsijxrevymwdqoq", "hkucgflaqwzwsijxrevymbdpoq", "nkjcgflathzvsijxrevyjbdpoq", "niucgflathzwsijxrezymbdpob", "ynucgflathzwsijxremymbdpoq", "nkubgflathzwsijxrhvymldpoq", "nkucqflrthzesijxrevymbdpoq", "nkucgulathzwsijxrevyubdioq", "nkuczflathzwsijxaebymbdpoq", "nkucgfldthzwsibxrevymrdpoq", "nkucgflatwzdsijxrevymsdpoq", "nkncgffathzwsijxrejymbdpoq", "nkucgflathzqsijxrevxmodpoq", "nkucgflathwwsijqrevymbipoq", "nkucgflathzwhajxrebymbdpoq", "gkucgflathzwsijxreirmbdpoq", "nkucgflathzesijzravymbdpoq", "nkucgflaghzwsijxrerymbdplq", "wkucgflathxwgijxrevymbdpoq", "nkucgfljthfwsijxrevymbdpfq", "nkucgflathwwsimxrevymbdpjq", "nkucgdlachzwsijxrevymmdpoq", "njucgclathzwsiixrevymbdpoq", "nkucgflatdzwsijxrevymzrpoq", "nkuckflatvzcsijxrevymbdpoq", "nkucgflathzhsijxrevqmbkpoq", "nkucqflathzjsijvrevymbdpoq", "nkucgftathzwsijxrevympdpoi", "nvucgflatmzwsijxrevymbdpsq", "nkocgflathznsijxrevymbdphq", "mkgcgflathzwsijxrevymbdpvq", "nkucnflathzwsijbrevymbdcoq", "nkucgflathzwsijsrevymsdgoq", "nkuckflatxzwsiwxrevymbdpoq", "nkucyflathzwsijxrehcmbdpoq", "nkurgflajhzwsijxrevkmbdpoq", "wkucgflathzwsijxrfvymbapoq", "nkucgflathzwsijxaekymbdpon", "nkucgfkathywsijxrevymbdpsq", "nkucgflathzwsijxaexcmbdpoq", "nkucgflathzwsijxrevymddhox", "nkucgflathzwgijxrevymydooq", "nkycqflathzwsijxrezymbdpoq", "nkucgflathwwsijxrevymbspsq", "nkucgflatpzwssjfrevymbdpoq", "nkwcgflhthzwsijxrevcmbdpoq" };

        [Fact]
        public void ActualDataTestPart1()
        {
            int twoCount = 0;
            int threeCount = 0;
            foreach (var id in actualData)
            {
                var result = InventoryManagementSystem.Scan(id);

                if (result.HasTwo)
                    twoCount++;

                if (result.HasThree)
                    threeCount++;
            }

            Assert.Equal(7221, twoCount * threeCount);
        }

        [Fact]
        public void ActualDataTestPart2()
        {
            IList<string> ids = new List<string>();
            for (int i = 0; i < actualData.Count; i++)
            {
                for (int j = i + 1; j < actualData.Count; j++)
                {
                    var count = InventoryManagementSystem.Compare(actualData[i], actualData[j]);

                    if (count == 1)
                    {
                        ids.Add(actualData[i]);
                        ids.Add(actualData[j]);
                    }
                }
            }

            Assert.Equal(2, ids.Count);

            var matchingIndex = InventoryManagementSystem.IndexOf(ids[0], ids[1]);

            Assert.Equal("mkcdflathzwsvjxrevymbdpoq", $"{ids[0].Substring(0, matchingIndex)}{ids[0].Substring(matchingIndex + 1, ids[0].Length - matchingIndex - 1)}");
        }

        [Fact]
        public void TestId1()
        {
            var result = InventoryManagementSystem.Scan("abcdef");

            Assert.False(result.HasTwo);
            Assert.False(result.HasThree);
        }

        [Fact]
        public void TestId2()
        {
            var result = InventoryManagementSystem.Scan("bababc");

            Assert.True(result.HasTwo);
            Assert.True(result.HasThree);
        }

        [Fact]
        public void TestId3()
        {
            var result = InventoryManagementSystem.Scan("abbcde");

            Assert.True(result.HasTwo);
            Assert.False(result.HasThree);
        }

        [Fact]
        public void TestId4()
        {
            var result = InventoryManagementSystem.Scan("abcccd");

            Assert.False(result.HasTwo);
            Assert.True(result.HasThree);
        }

        [Fact]
        public void TestId5()
        {
            var result = InventoryManagementSystem.Scan("aabcdd");

            Assert.True(result.HasTwo);
            Assert.False(result.HasThree);
        }

        [Fact]
        public void TestId6()
        {
            var result = InventoryManagementSystem.Scan("abcdee");

            Assert.True(result.HasTwo);
            Assert.False(result.HasThree);
        }

        [Fact]
        public void TestId7()
        {
            var result = InventoryManagementSystem.Scan("ababab");

            Assert.False(result.HasTwo);
            Assert.True(result.HasThree);
        }

        [Fact]
        public void CompareIdsOneDifference()
        {
            var result = InventoryManagementSystem.Compare("abc", "aba");

            Assert.Equal(1, result);
        }

        [Fact]
        public void CompareIdsTwoDifferences()
        {
            var result = InventoryManagementSystem.Compare("abc", "ade");

            Assert.Equal(2, result);
        }

        [Fact]
        public void CompareIdsNotSame()
        {
            var result = InventoryManagementSystem.Compare("xyz", "adc");

            Assert.Equal(3, result);
        }
    }
}
