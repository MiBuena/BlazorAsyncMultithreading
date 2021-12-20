﻿using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using PopulationCensus.Server.Interfaces;
using PopulationCensus.Server.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationCensus.UnitTests.LocalFileServiceTests
{
    public class ReadFileInPortionsAsyncTests
    {
        [Test]
        public async Task Should_ReturnOneCollection_When_ContentWithTwoLinesAsync()
        {
            //Arrange
            string fakeFileContents = "Code,Description,SortOrder\r\n888,Median age,2";
            byte[] fakeFileBytes = Encoding.UTF8.GetBytes(fakeFileContents);
            MemoryStream fakeMemoryStream = new MemoryStream(fakeFileBytes);

            Mock<IStreamReaderWrapper> streamReaderWrapperMock = new Mock<IStreamReaderWrapper>();
            streamReaderWrapperMock.Setup(fileManager => fileManager.GetStreamReader(It.IsAny<IFormFile>()))
                           .Returns(() => new StreamReader(fakeMemoryStream));

            Mock<IFormFile> fileFormMock = new Mock<IFormFile>();
            var localFileService = new LocalFileService(streamReaderWrapperMock.Object);

            //Act
            var collections = localFileService.ReadFileInPortionsAsync(fileFormMock.Object);

            var result = new List<IEnumerable<string>>();
            await foreach (var collection in collections)
            {
                result.Add(collection);
            }

            //Assert
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public async Task Should_ReturnFirstLineAfterTitle_When_ContentWithTwoLines()
        {
            //Arrange
            string fakeFileContents = "Code,Description,SortOrder\r\n888,Median age,2";
            byte[] fakeFileBytes = Encoding.UTF8.GetBytes(fakeFileContents);
            MemoryStream fakeMemoryStream = new MemoryStream(fakeFileBytes);

            Mock<IStreamReaderWrapper> streamReaderWrapperMock = new Mock<IStreamReaderWrapper>();
            streamReaderWrapperMock.Setup(fileManager => fileManager.GetStreamReader(It.IsAny<IFormFile>()))
                           .Returns(() => new StreamReader(fakeMemoryStream));

            Mock<IFormFile> fileFormMock = new Mock<IFormFile>();

            var localFileService = new LocalFileService(streamReaderWrapperMock.Object);

            //Act
            var collections = localFileService.ReadFileInPortionsAsync(fileFormMock.Object);


            var result = new List<string>();
            await foreach (var collection in collections)
            {
                foreach (var line in collection)
                {
                    result.Add(line);
                }
            }

            //Assert
            Assert.AreEqual("888,Median age,2", result.FirstOrDefault());
        }

        [Test]
        public async Task Should_ReturnThreeCollections_When_ContentWith2387Lines()
        {
            //Arrange
            var fakeFileContents = GetLongContent();
            byte[] fakeFileBytes = Encoding.UTF8.GetBytes(fakeFileContents);
            MemoryStream fakeMemoryStream = new MemoryStream(fakeFileBytes);

            Mock<IStreamReaderWrapper> streamReaderWrapperMock = new Mock<IStreamReaderWrapper>();
            streamReaderWrapperMock.Setup(fileManager => fileManager.GetStreamReader(It.IsAny<IFormFile>()))
                           .Returns(() => new StreamReader(fakeMemoryStream));

            Mock<IFormFile> fileFormMock = new Mock<IFormFile>();
            var localFileService = new LocalFileService(streamReaderWrapperMock.Object);

            //Act
            var collections = localFileService.ReadFileInPortionsAsync(fileFormMock.Object);

            var result = new List<IEnumerable<string>>();
            await foreach (var collection in collections)
            {
                result.Add(collection);
            }

            //Assert
            Assert.AreEqual(3, result.Count);
        }

        [Test]
        public async Task Should_ReturnFirstCollectionWithCount1000_When_ContentWith2387Lines()
        {
            //Arrange
            var fakeFileContents = GetLongContent();
            byte[] fakeFileBytes = Encoding.UTF8.GetBytes(fakeFileContents);
            MemoryStream fakeMemoryStream = new MemoryStream(fakeFileBytes);

            Mock<IStreamReaderWrapper> streamReaderWrapperMock = new Mock<IStreamReaderWrapper>();
            streamReaderWrapperMock.Setup(fileManager => fileManager.GetStreamReader(It.IsAny<IFormFile>()))
                           .Returns(() => new StreamReader(fakeMemoryStream));

            Mock<IFormFile> fileFormMock = new Mock<IFormFile>();
            var localFileService = new LocalFileService(streamReaderWrapperMock.Object);

            //Act
            var collections = localFileService.ReadFileInPortionsAsync(fileFormMock.Object);

            var result = new List<IEnumerable<string>>();
            await foreach (var collection in collections)
            {
                result.Add(collection);
            }

            //Assert
            Assert.AreEqual(1000, result.First().Count());
        }

        [Test]
        public async Task Should_ReturnSecondCollectionWithCount1000_When_ContentWith2387Lines()
        {
            //Arrange
            var fakeFileContents = GetLongContent();
            byte[] fakeFileBytes = Encoding.UTF8.GetBytes(fakeFileContents);
            MemoryStream fakeMemoryStream = new MemoryStream(fakeFileBytes);

            Mock<IStreamReaderWrapper> streamReaderWrapperMock = new Mock<IStreamReaderWrapper>();
            streamReaderWrapperMock.Setup(fileManager => fileManager.GetStreamReader(It.IsAny<IFormFile>()))
                           .Returns(() => new StreamReader(fakeMemoryStream));

            Mock<IFormFile> fileFormMock = new Mock<IFormFile>();
            var localFileService = new LocalFileService(streamReaderWrapperMock.Object);

            //Act
            var collections = localFileService.ReadFileInPortionsAsync(fileFormMock.Object);

            var result = new List<IEnumerable<string>>();
            await foreach (var collection in collections)
            {
                result.Add(collection);
            }

            //Assert
            Assert.AreEqual(1000, result.Skip(1).First().Count());
        }


        [Test]
        public async Task Should_ReturnThirdCollectionWithCount387_When_ContentWith2386Lines()
        {
            //Arrange
            var fakeFileContents = GetLongContent();
            byte[] fakeFileBytes = Encoding.UTF8.GetBytes(fakeFileContents);
            MemoryStream fakeMemoryStream = new MemoryStream(fakeFileBytes);

            Mock<IStreamReaderWrapper> streamReaderWrapperMock = new Mock<IStreamReaderWrapper>();
            streamReaderWrapperMock.Setup(fileManager => fileManager.GetStreamReader(It.IsAny<IFormFile>()))
                           .Returns(() => new StreamReader(fakeMemoryStream));

            Mock<IFormFile> fileFormMock = new Mock<IFormFile>();
            var localFileService = new LocalFileService(streamReaderWrapperMock.Object);

            //Act
            var collections = localFileService.ReadFileInPortionsAsync(fileFormMock.Object);

            var result = new List<IEnumerable<string>>();
            await foreach (var collection in collections)
            {
                result.Add(collection);
            }

            //Assert
            Assert.AreEqual(386, result.Skip(2).First().Count());
        }

        [Test]
        public async Task Should_ReturnCorrectContentForFirstCollection_When_ContentWith2387Lines()
        {
            //Arrange
            var fakeFileContents = GetLongContent();
            byte[] fakeFileBytes = Encoding.UTF8.GetBytes(fakeFileContents);
            MemoryStream fakeMemoryStream = new MemoryStream(fakeFileBytes);

            Mock<IStreamReaderWrapper> streamReaderWrapperMock = new Mock<IStreamReaderWrapper>();
            streamReaderWrapperMock.Setup(fileManager => fileManager.GetStreamReader(It.IsAny<IFormFile>()))
                           .Returns(() => new StreamReader(fakeMemoryStream));

            Mock<IFormFile> fileFormMock = new Mock<IFormFile>();
            var localFileService = new LocalFileService(streamReaderWrapperMock.Object);

            //Act
            var collections = localFileService.ReadFileInPortionsAsync(fileFormMock.Object);

            var result = await collections.FirstAsync();
            var resultAsList = result.ToList();
            var first1000LinesAsCollection = GetFirst1000RowsContentAsCollection();

            //Assert
            for (int i = 0; i < result.Count(); i++)
            {
                Assert.AreEqual(first1000LinesAsCollection[i], resultAsList[i]);
            }
        }

        private IList<string> GetFirst1000RowsContentAsCollection()
        {
            string content = @"9999,Total - New Zealand by Regional Council/SA2,1
999999,Total - New Zealand by Territorial Authority/SA2,2
DHB9999,Total - New Zealand by District Health Board,3
01,Northland Region,4
02,Auckland Region,5
03,Waikato Region,6
04,Bay of Plenty Region,7
05,Gisborne Region,8
06,Hawke's Bay Region,9
07,Taranaki Region,10
08,Manawatu-Wanganui Region,11
09,Wellington Region,12
16,Tasman Region,13
17,Nelson Region,14
18,Marlborough Region,15
12,West Coast Region,16
13,Canterbury Region,17
14,Otago Region,18
15,Southland Region,19
77,Total - Regional Council Areas,20
99,Area Outside Region,21
001,Far North District,22
002,Whangarei District,23
003,Kaipara District,24
076,Auckland,25
CMB07601,Rodney Local Board Area,26
CMB07602,Hibiscus and Bays Local Board Area,27
CMB07603,Upper Harbour Local Board Area,28
CMB07604,Kaipatiki Local Board Area,29
CMB07605,Devonport-Takapuna Local Board Area,30
CMB07606,Henderson-Massey Local Board Area,31
CMB07607,Waitakere Ranges Local Board Area,32
CMB07608,Great Barrier Local Board Area,33
CMB07609,Waiheke Local Board Area,34
CMB07610,Waitemata Local Board Area,35
CMB07611,Whau Local Board Area,36
CMB07612,Albert-Eden Local Board Area,37
CMB07613,Puketapapa Local Board Area,38
CMB07614,Orakei Local Board Area,39
CMB07615,Maungakiekie-Tamaki Local Board Area,40
CMB07616,Howick Local Board Area,41
CMB07617,Mangere-Otahuhu Local Board Area,42
CMB07618,Otara-Papatoetoe Local Board Area,43
CMB07619,Manurewa Local Board Area,44
CMB07620,Papakura Local Board Area,45
CMB07621,Franklin Local Board Area,46
011,Thames-Coromandel District,47
012,Hauraki District,48
013,Waikato District,49
015,Matamata-Piako District,50
016,Hamilton City,51
017,Waipa District,52
018,Otorohanga District,53
019,South Waikato District,54
020,Waitomo District,55
021,Taupo District,56
022,Western Bay of Plenty District,57
023,Tauranga City,58
024,Rotorua District,59
025,Whakatane District,60
026,Kawerau District,61
027,Opotiki District,62
028,Gisborne District,63
029,Wairoa District,64
030,Hastings District,65
031,Napier City,66
032,Central Hawke's Bay District,67
033,New Plymouth District,68
034,Stratford District,69
035,South Taranaki District,70
036,Ruapehu District,71
037,Whanganui District,72
038,Rangitikei District,73
039,Manawatu District,74
040,Palmerston North City,75
041,Tararua District,76
042,Horowhenua District,77
043,Kapiti Coast District,78
044,Porirua City,79
045,Upper Hutt City,80
046,Lower Hutt City,81
047,Wellington City,82
048,Masterton District,83
049,Carterton District,84
050,South Wairarapa District,85
051,Tasman District,86
052,Nelson City,87
053,Marlborough District,88
054,Kaikoura District,89
055,Buller District,90
056,Grey District,91
057,Westland District,92
058,Hurunui District,93
059,Waimakariri District,94
060,Christchurch City,95
062,Selwyn District,96
063,Ashburton District,97
064,Timaru District,98
065,Mackenzie District,99
066,Waimate District,100
067,Chatham Islands Territory,101
068,Waitaki District,102
069,Central Otago District,103
070,Queenstown-Lakes District,104
071,Dunedin City,105
072,Clutha District,106
073,Southland District,107
074,Gore District,108
075,Invercargill City,109
777,Total - Territorial Authority areas,110
999,Area Outside Territorial Authority,111
100100,North Cape,112
100200,Rangaunu Harbour,113
100300,Inlets Far North District,114
100400,Karikari Peninsula,115
100500,Tangonge,116
100600,Ahipara,117
100700,Kaitaia East,118
100800,Kaitaia West,119
100900,Rangitihi,120
101000,Oruru-Parapara,121
101100,Taumarumaru,122
101200,Herekino-Takahue,123
101300,Peria,124
101400,Taemaro-Oruaiti,125
101500,Whakapaku,126
101600,Hokianga North,127
101700,Kohukohu-Broadwood,128
101800,Whakarara,129
101900,Kaeo,130
102000,Omahuta Forest-Horeke,131
102100,Hokianga South,132
102200,Lake Manuwai-Kapiro,133
102300,Okaihau,134
102400,Rangitane-Purerua,135
102500,Waipapa,136
102600,Puketotara,137
102700,Waima Forest,138
102800,Riverview,139
102900,Waipoua Forest,140
103000,Kerikeri Central,141
103100,Kerikeri South,142
103200,Ohaeawai-Waimate North,143
103300,Puketona-Waitangi,144
103400,Ngapuhi,145
103500,Kaikohe,146
103600,Pakaraka,147
103700,Haruru,148
103800,Russell,149
103900,Paihia,150
104000,Mataraua Forest,151
104100,Matawaia-Taumarere,152
104200,Russell Peninsula,153
104300,Opua (Far North District),154
104400,Russell Forest-Rawhiti,155
104500,Moerewa,156
104600,Kawakawa,157
104700,Maromaku,158
104800,Mangakahia-Hukerenui,159
104900,Whangaruru,160
105000,Inlets other Whangarei District,161
105100,Matarau,162
105200,Hikurangi,163
105300,Kauri,164
105400,Maungatapere,165
105500,Kamo West,166
105600,Matapouri-Tutukaka,167
105700,Kiripaka,168
105800,Kamo East,169
105900,Granfield Reserve,170
106000,Kamo Central,171
106100,Whau Valley,172
106200,Tikipunga North,173
106300,Pukenui,174
106400,Otangarei,175
106500,Tikipunga South,176
106600,Kensington (Whangarei District),177
106700,Abbey Caves-Glenbervie,178
106800,Mairtown,179
106900,Maunu-Horahora,180
107000,Woodhill-Vinetown,181
107100,Whangarei Central,182
107200,Riverside,183
107300,Raumanga,184
107400,Tarewa,185
107500,Morningside (Whangarei District),186
107600,Ngunguru,187
107700,Otaika-Portland,188
107800,Oakleigh-Mangapai,189
107900,Port-Limeburners,190
108000,Pataua,191
108100,Onerahi Park,192
108200,Sherwood Rise,193
108300,Onerahi,194
108400,Inlet Whangarei Harbour,195
108500,Parua Bay,196
108600,Bream Bay,197
108700,Marsden Bay,198
108800,Ruakaka,199
108900,Bream Head,200
109000,Waipu,201
109100,Kaipara Coastal,202
109200,Maungaru,203
109300,Dargaville,204
109400,Ruawai-Matakohe,205
109500,Otamatea (Kaipara District),206
109600,Maungaturoto,207
109700,Kaiwaka,208
109800,Mangawhai Rural,209
109900,Mangawhai Heads,210
110000,Inlet Mangawhai Harbour,211
110100,Mangawhai,212
258200,Oceanic Northland Region,213
258300,Inlet Doubtless Bay,214
258400,Inlet Kaipara Harbour North,215
110200,Okahukura Peninsula,216
110300,Inlet Kaipara Harbour South,217
110400,Cape Rodney,218
110500,Wellsford,219
110600,Oceanic Auckland Region East,220
110700,South Head,221
110800,Kaipara Hills,222
110900,Dome Valley-Matakana,223
111000,Oceanic Auckland Region West,224
111100,Warkworth West,225
111200,Puhoi Valley,226
111300,Warkworth East,227
111400,Sandspit,228
111500,Tawharanui Peninsula,229
111600,Te Kuru,230
111700,Snells Beach,231
111800,Barrier Islands,232
111900,Algies Bay-Scotts Landing,233
112000,Inlets other Auckland,234
112100,Wainui-Waiwera,235
112200,Parakai,236
112300,Helensville Rural,237
112400,Helensville,238
112500,Waitoki,239
112600,Waikoukou Valley,240
112700,Orewa North,241
112800,Hatfields Beach,242
112900,Orewa South,243
113000,Orewa Central,244
113100,Millwater North,245
113200,Waipatukahu,246
113300,Millwater South,247
113400,Dairy Flat North,248
113500,Dairy Flat West,249
113600,Kingsway,250
113700,Riverhead Forest,251
113800,Silverdale Central (Auckland),252
113900,Red Beach West,253
114000,Red Beach East,254
114100,Silverdale South (Auckland),255
114200,Waimauku,256
114300,Gulf Islands,257
114400,Vipond,258
114500,Muriwai,259
114600,Stanmore Bay West,260
114700,Kumeu Rural West,261
114800,Okura Bush,262
114900,Whangaparoa Central,263
115000,Kumeu-Huapai,264
115100,Stanmore Bay East,265
115200,Manly West,266
115300,Muriwai Valley-Bethells Beach,267
115400,Tindalls-Matakatia,268
115500,Coatesville,269
115600,Dairy Flat South,270
115700,Gulf Harbour North,271
115800,Manly East,272
115900,Riverhead,273
116000,Army Bay,274
116100,Kumeu Rural East,275
116200,Gulf Harbour South,276
116300,Paremoremo West,277
116400,Taupaki,278
116500,Long Bay,279
116600,Paremoremo East,280
116700,Albany Heights,281
116800,Awaruku,282
116900,Fairview Heights,283
117000,Whenuapai,284
117100,Waitakere Ranges North,285
117200,Torbay,286
117300,Albany Central,287
117400,Albany West,288
117500,Glamorgan,289
117600,Oteha East,290
117700,Northcross,291
117800,Oteha West,292
117900,Waiake,293
118000,Waitakere,294
118100,Albany South,295
118200,Browns Bay South West,296
118300,Browns Bay Central,297
118400,Schnapper Rock,298
118500,Pinehill,299
118600,North Harbour,300
118700,Browns Bay South East,301
118800,Westgate Central,302
118900,Greenhithe West,303
119000,Rothesay Bay,304
119100,Greenhithe East,305
119200,Hobsonville,306
119300,Inlet Waitemata Harbour,307
119400,Greenhithe South,308
119500,Murrays Bay West,309
119600,Westgate South,310
119700,Windsor Park,311
119800,West Harbour West,312
119900,Unsworth Heights West,313
120000,Murrays Bay East,314
120100,Birdwood West,315
120200,Hobsonville Point,316
120300,West Harbour Clearwater Cove,317
120400,Unsworth Heights East,318
120500,Mairangi Bay North,319
120600,Massey Central,320
120700,West Harbour Luckens Point,321
120800,Mairangi Bay South,322
120900,Massey Royal Road West,323
121000,Bayview East,324
121100,Swanson Rural,325
121200,Sunnynook South,326
121300,Sunnynook North,327
121400,Bayview West,328
121500,Totara Vale North,329
121600,Royal Heights North,330
121700,Massey West,331
121800,Beach Haven West,332
121900,Totara Vale South,333
122000,Swanson,334
122100,Bayview South,335
122200,Campbells Bay,336
122300,Royal Heights South,337
122400,Beach Haven East,338
122500,Massey East,339
122600,Forrest Hill North,340
122700,Glenfield North,341
122800,Birkdale North,342
122900,Massey South,343
123000,Ranui North,344
123100,Glenfield West,345
123200,Te Atatu Peninsula North West,346
123300,Glenfield South West,347
123400,Forrest Hill West,348
123500,Wairau Valley,349
123600,Forrest Hill East,350
123700,Castor Bay,351
123800,Ranui Domain,352
123900,Beach Haven South,353
124000,Glenfield Central,354
124100,Piha,355
124200,Ranui South West,356
124300,Henderson Larnoch,357
124400,Birkdale South,358
124500,Te Atatu Peninsula Central,359
124600,Glenfield East,360
124700,Henderson Valley,361
124800,Te Atatu Peninsula West,362
124900,Henderson Lincoln West,363
125000,Ranui South East,364
125100,Henderson Lincoln East,365
125200,Birkenhead West,366
125300,Te Atatu Peninsula East,367
125400,Milford West,368
125500,Westlake,369
125600,Hillcrest North (Auckland),370
125700,Henderson Lincoln South,371
125800,Hillcrest West (Auckland),372
125900,Milford Central,373
126000,Summerland South,374
126100,Summerland North,375
126200,Hillcrest East (Auckland),376
126300,Chatswood,377
126400,Te Atatu South-Edmonton,378
126500,Birkenhead North,379
126600,Takapuna West,380
126700,Henderson North,381
126800,Takapuna Central,382
126900,Western Heights (Auckland),383
127000,Te Atatu South-Central,384
127100,Northcote Central (Auckland),385
127200,Akoranga,386
127300,Northcote South (Auckland),387
127400,Birkenhead South,388
127500,Henderson Central,389
127600,Waiatarua,390
127700,Te Atatu South-McLeod North,391
127800,Henderson Valley Park,392
127900,Henderson North East,393
128000,Waitakere Ranges South,394
128100,Northcote Tuff Crater,395
128200,Northcote Point (Auckland),396
128300,McLaren Park,397
128400,Henderson East,398
128500,Te Atatu South-McLeod South,399
128600,Takapuna South,400
128700,Rosebank Peninsula,401
128800,Hauraki,402
128900,Glendene North,403
129000,Sunnyvale West-Parrs Park,404
129100,Sunnyvale East,405
129200,Point Chevalier West,406
129300,Belmont (Auckland),407
129400,Bayswater,408
129500,Glendene South,409
129600,Glen Eden West,410
129700,Herne Bay,411
129800,Westmere North,412
129900,Point Chevalier East,413
130000,Oratia,414
130100,Glen Eden Rosier,415
130200,Saint Marys Bay,416
130300,Westmere South-Western Springs,417
130400,Ponsonby West,418
130500,Kelston North,419
130600,Avondale Rosebank,420
130700,Narrow Neck,421
130800,Glen Eden North,422
130900,Glen Eden Woodglen,423
131000,Kelston South,424
131100,Waterview,425
131200,Ponsonby East,426
131300,Wynyard-Viaduct,427
131400,Stanley Point,428
131500,Glen Eden Central,429
131600,Grey Lynn North,430
131700,Avondale West (Auckland),431
131800,Freemans Bay,432
131900,Mount Albert West,433
132000,Avondale North (Auckland),434
132100,Grey Lynn West,435
132200,Mount Albert North,436
132300,New Lynn North,437
132400,Victoria Park,438
132500,Konini,439
132600,New Lynn North West,440
132700,Hobson Ridge North,441
132800,Grey Lynn Central,442
132900,Fruitvale,443
133000,Devonport,444
133100,Avondale Central (Auckland),445
133200,Queen Street,446
133300,Quay Street-Customs Street,447
133400,Hobson Ridge Central,448
133500,Grey Lynn East,449
133600,Morningside (Auckland),450
133700,Shortland Street,451
133800,Hobson Ridge South,452
133900,New Lynn Central,453
134000,Cheltenham,454
134100,Queen Street South West,455
134200,Mount Albert Central,456
134300,Karangahape,457
134400,Kingsland,458
134500,Anzac Avenue,459
134600,Waima-Woodlands Park,460
134700,Kaurilands,461
134800,Auckland-University,462
134900,New Lynn Seabrook,463
135000,Avondale South (Auckland),464
135100,Symonds Street North West,465
135200,Mount Albert South,466
135300,Symonds Street West,467
135400,West Lynn,468
135500,Owairaka West,469
135600,New Lynn Central South,470
135700,The Strand,471
135800,St Lukes,472
135900,Symonds Street East,473
136000,Eden Terrace,474
136100,Grafton,475
136200,Eden Park,476
136300,Sandringham North,477
136400,Parnell West,478
136500,New Lynn South,479
136600,New Windsor North,480
136700,Titirangi South,481
136800,Owairaka East,482
136900,Eden Valley,483
137000,Sandringham Central,484
137100,Glenavon,485
137200,Blockhouse Bay North,486
137300,Green Bay North,487
137400,Parnell East,488
137500,Laingholm,489
137600,Sandringham West,490
137700,Mount Eden North East,491
137800,Mount Eden North,492
137900,Wesley West,493
138000,Green Bay South,494
138100,Balmoral,495
138200,Sandringham East,496
138300,New Windsor South,497
138400,Mount Eden West,498
138500,Newmarket,499
138600,Blockhouse Bay North East,500
138700,Epsom North,501
138800,Wesley South,502
138900,Blockhouse Bay South,503
139000,Mount Eden East,504
139100,Wesley East,505
139200,Orakei West,506
139300,Maungawhau,507
139400,Remuera West,508
139500,Blockhouse Bay East,509
139600,Lynfield North,510
139700,Mount Roskill North,511
139800,Mount Roskill White Swan,512
139900,Mount Eden South,513
140000,Remuera Waitaramoa,514
140100,Orakei East,515
140200,Epsom Central-North,516
140300,Mount St John,517
140400,Mount Roskill Central North,518
140500,Remuera North,519
140600,Three Kings West,520
140700,Mount Roskill Central South,521
140800,Epsom Central-South,522
140900,Lynfield South,523
141000,Remuera South,524
141100,Mission Bay,525
141200,Three Kings East,526
141300,Mount Roskill South,527
141400,Inlet Manukau Harbour,528
141500,Epsom East,529
141600,Remuera Waiata,530
141700,Epsom South,531
141800,Kohimarama,532
141900,Meadowbank West,533
142000,Hillsborough North (Auckland),534
142100,Hillsborough West (Auckland),535
142200,Greenlane North,536
142300,Waikowhai North,537
142400,Remuera East,538
142500,Waikowhai South,539
142600,Royal Oak West (Auckland),540
142700,Saint Heliers West,541
142800,One Tree Hill,542
142900,Hillsborough East (Auckland),543
143000,Saint Heliers North,544
143100,Remuera Abbotts Park,545
143200,Meadowbank East,546
143300,Ellerslie Central,547
143400,Royal Oak East (Auckland),548
143500,Saint Heliers South,549
143600,Hillsborough South (Auckland),550
143700,Saint Johns West,551
143800,Greenlane South,552
143900,Remuera Waiatarua,553
144000,Glendowie North,554
144100,Onehunga West,555
144200,Ellerslie West,556
144300,Onehunga North,557
144400,Ellerslie East,558
144500,Saint Johns East,559
144600,Oneroa West,560
144700,Glen Innes West,561
144800,Ellerslie South,562
144900,Stonefields West,563
145000,Onehunga Central,564
145100,Glendowie South,565
145200,Oranga,566
145300,Mount Wellington North West,567
145400,Panmure Glen Innes Industrial,568
145500,Onehunga-Te Papapa Industrial,569
145600,Te Papapa,570
145700,Stonefields East,571
145800,Glen Innes East-Wai O Taiki Bay,572
145900,Penrose,573
146000,Mount Wellington North East,574
146100,Mangere Bridge Ambury,575
146200,Mount Wellington Ferndale,576
146300,Point England,577
146400,Bucklands Beach North,578
146500,Mount Wellington East,579
146600,Mount Wellington West,580
146700,Panmure West,581
146800,Mangere Bridge,582
146900,Oneroa East-Palm Beach,583
147000,Tamaki,584
147100,Panmure East,585
147200,Mount Wellington Central,586
147300,Bays Waiheke Island,587
147400,Sylvia Park,588
147500,Mangere Mountain View,589
147600,Surfdale,590
147700,Mount Wellington Industrial,591
147800,Half Moon Bay West,592
147900,Auckland Airport,593
148000,Eastern Beach,594
148100,Sunnyhills West-Pakuranga North,595
148200,Favona North,596
148300,Bucklands Beach Central,597
148400,Farm Cove,598
148500,Mangere North,599
148600,Favona West,600
148700,Ostend,601
148800,Half Moon Bay North East,602
148900,Mangere West,603
149000,Pakuranga West,604
149100,Mount Wellington South West,605
149200,Sunnyhills East,606
149300,Half Moon Bay South East,607
149400,Favona East,608
149500,Mount Wellington South East,609
149600,Otahuhu Industrial,610
149700,Bucklands Beach South,611
149800,Pakuranga Central,612
149900,Awhitu,613
150000,Harania North,614
150100,Otahuhu Central,615
150200,Mangere Central,616
150300,Otahuhu North,617
150400,Howick West,618
150500,Pakuranga Heights North West,619
150600,Mellons Bay,620
150700,Pakuranga Heights East,621
150800,Sutton Park,622
150900,Otahuhu East,623
151000,Mangere South,624
151100,Onetangi,625
151200,Harania South,626
151300,Highland Park (Auckland),627
151400,Otahuhu South West,628
151500,Pakuranga Heights South West,629
151600,Mangere Mascot,630
151700,Waiheke East,631
151800,Massey Road West,632
151900,Otahuhu South,633
152000,Howick Central,634
152100,Massey Road North,635
152200,Botany Downs West,636
152300,East Tamaki,637
152400,Howick East,638
152500,Burswood,639
152600,Massey Road South,640
152700,Middlemore,641
152800,Mangere South East,642
152900,Botany Downs East,643
153000,Grange,644
153100,Golflands,645
153200,Aorere North,646
153300,Mangere East,647
153400,Cockle Bay,648
153500,Otara West,649
153600,Aorere Central,650
153700,Northpark North,651
153800,Somerville,652
153900,Northpark South,653
154000,Papatoetoe North,654
154100,Dingwall,655
154200,Aorere South,656
154300,Shelly Park,657
154400,Otara Central,658
154500,Huntington Park,659
154600,Botany Central,660
154700,Botany North,661
154800,Papatoetoe West,662
154900,Papatoetoe Central,663
155000,Sunkist Bay,664
155100,Otara East,665
155200,Redcastle,666
155300,Botany East,667
155400,Botany South,668
155500,Manukau Central,669
155600,Papatoetoe East,670
155700,Otara South,671
155800,Ferguson,672
155900,Papatoetoe South West,673
156000,Botany Junction,674
156100,Dannemora North,675
156200,Papatoetoe South,676
156300,East Tamaki Heights,677
156400,Te Puru,678
156500,Puhinui North,679
156600,Dannemora South,680
156700,Puhinui South,681
156800,Rongomai West,682
156900,Baverstock,683
157000,Puhinui East,684
157100,Turanga,685
157200,Rongomai East,686
157300,Ormiston North,687
157400,Maraetai,688
157500,Chapel Downs,689
157600,Wiri West,690
157700,Clover Park North,691
157800,Mission Heights North,692
157900,Clover Park South,693
158000,Donegal Park,694
158100,Ormiston South,695
158200,Mission Heights South,696
158300,Hilltop (Auckland),697
158400,Wiri East,698
158500,Goodwood Heights,699
158600,Ormiston East,700
158700,Clendon Park North,701
158800,Burbank,702
158900,Tuscany Heights,703
159000,Totara Heights,704
159100,Homai East,705
159200,Homai West,706
159300,The Gardens (Auckland),707
159400,Clarks Beach,708
159500,Clendon Park West,709
159600,Hillpark North,710
159700,Clendon Park East,711
159800,Karaka Creek,712
159900,Rowandale West,713
160000,Rowandale East,714
160100,Manurewa Central,715
160200,Weymouth North,716
160300,Hillpark South,717
160400,Alfriston,718
160500,Weymouth East,719
160600,Leabank,720
160700,Weymouth South,721
160800,Manurewa East,722
160900,Manurewa South,723
161000,Randwick Park East,724
161100,Wattle Downs West,725
161200,Wattle Downs North,726
161300,Randwick Park West,727
161400,Kingseat-Karaka,728
161500,Wattle Downs East,729
161600,Takanini North,730
161700,Takanini Industrial,731
161800,Conifer Grove West,732
161900,Conifer Grove East,733
162000,Takanini West,734
162100,Takanini South,735
162200,Takanini Central,736
162300,Ardmore,737
162400,Glenbrook,738
162500,Hingaia,739
162600,Takanini South East,740
162700,Kawakawa Bay-Orere,741
162800,Papakura West,742
162900,Clevedon,743
163000,Papakura North,744
163100,Pahurehure,745
163200,Papakura Central,746
163300,Papakura North East,747
163400,Karaka Lakes,748
163500,Papakura Kelvin,749
163600,Papakura Massey Park,750
163700,Rosehill,751
163800,Patumahoe,752
163900,Papakura East,753
164000,Opaheke,754
164100,Papakura Industrial,755
164200,Red Hill,756
164300,Drury,757
164400,Tamakae,758
164500,Hamilton Estate,759
164600,Drury Rural,760
164700,Waiuku Central,761
164800,Waiuku East,762
164900,Kendallvale,763
165000,Puni,764
165100,Ramarama,765
165200,Hunua,766
165300,Pukekohe North West,767
165400,Anselmi Ridge,768
165500,Pukekohe West,769
165600,Cape Hill,770
165700,Rosa Birch Park,771
165800,Rooseville Park,772
165900,Cloverlea (Auckland),773
166000,Pukekohe Central,774
166100,Pukekohe Hospital,775
166200,Buckland,776
166300,Bombay Hills,777
166400,Ararimu,778
166500,Colville,779
166600,Islands Thames-Coromandel District,780
166700,Coromandel,781
166800,Inlets Thames-Coromandel District,782
166900,Mercury Bay North,783
167000,Whitianga North,784
167100,Whitianga South,785
167200,Thames Coast,786
167300,Cooks Beach-Ferry Landing,787
167400,Mercury Bay South,788
167500,Kauaeranga,789
167600,Thames North,790
167700,Thames Central,791
167800,Thames South,792
167900,Hikuai,793
168000,Totora-Kopu,794
168100,Tairua,795
168200,Pauanui,796
168300,Matatoki-Puriri,797
168400,Whangamata Rural,798
168500,Whangamata,799
168600,Miranda-Pukorokoro,800
168700,Hauraki Plains North,801
168800,Hauraki Plains East,802
168900,Ngatea,803
169000,Hauraki Plains South,804
169100,Paeroa Rural,805
169200,Paeroa,806
169300,Waihi Rural,807
169400,Waihi North,808
169500,Waihi East,809
169600,Waihi South,810
169700,Aka Aka,811
169800,Mangatangi,812
169900,Tuakau Rural,813
170000,Tuakau North,814
170100,Onewhero,815
170200,Pokeno Rural,816
170300,Tuakau South,817
170400,Port Waikato-Waikaretu,818
170500,Pokeno,819
170600,Pukekawa,820
170700,Maramarua,821
170800,Rangiriri,822
170900,Te Akau,823
171000,Inlets Waikato District,824
171100,Te Kauwhata,825
171200,Huntly Rural,826
171300,Waerenga,827
171400,Huntly West,828
171500,Huntly East,829
171600,Raglan,830
171700,Whitikahu,831
171800,Te Uku,832
171900,Taupiri-Lake Kainui,833
172000,Ngaruawahia North,834
172100,Ngaruawahia Central,835
172200,Ngaruawahia South,836
172300,Kainui-Gordonton,837
172400,Te Kowhai,838
172500,Whatawhata West,839
172600,Horotiu,840
172700,Horsham Downs,841
172800,Whatawhata East,842
172900,Rotokauri,843
173000,Hamilton Park,844
173100,Eureka-Tauwhare,845
173200,Tamahere North,846
173300,Pukemoremore,847
173400,Tamahere South,848
173500,Tahuna-Mangateparu,849
173600,Mangaiti,850
173700,Tatuanui,851
173800,Tahuroa,852
173900,Morrinsville East,853
174000,Morrinsville West,854
174100,Te Aroha East,855
174200,Te Aroha West,856
174300,Waihou-Manawaru,857
174400,Waitoa-Ngarua,858
174500,Richmond Downs-Wardville,859
174600,Waharoa-Peria,860
174700,Okauia,861
174800,Hinuera,862
174900,Matamata North,863
175000,Matamata South,864
175100,Te Poi,865
175200,Te Rapa North,866
175300,Flagstaff North,867
175400,Rotokauri-Waiwhakareke,868
175500,Flagstaff South,869
175600,Rototuna North,870
175700,Pukete West,871
175800,Flagstaff East,872
175900,Rototuna Central,873
176000,Pukete East,874
176100,Te Manatu,875
176200,Rototuna South,876
176300,Te Rapa South,877
176400,Saint Andrews West,878
176500,Saint Andrews East,879
176600,Queenwood (Hamilton City),880
176700,St James,881
176800,Crawshaw,882
176900,Huntington,883
177000,Western Heights (Hamilton City),884
177100,Nawton West,885
177200,Nawton East,886
177300,Chartwell,887
177400,Forest Lake (Hamilton City),888
177500,Chedworth,889
177600,Beerescourt,890
177700,Miropiko,891
177800,Porritt,892
177900,Dinsdale North,893
178000,Maeroa,894
178100,Dinsdale South,895
178200,Fairfield (Hamilton City),896
178300,Whitiora,897
178400,Enderley North,898
178500,Fairview Downs,899
178600,Temple View,900
178700,Swarbrick,901
178800,Kahikatea,902
178900,Frankton Junction,903
179000,Kirikiriroa,904
179100,Enderley South,905
179200,Ruakura,906
179300,Claudelands,907
179400,Hamilton Central,908
179500,Hamilton Lake,909
179600,Peachgrove,910
179700,Hamilton East Village,911
179800,Hamilton West,912
179900,Greensboro,913
180000,Hamilton East Cook,914
180100,Melville North,915
180200,Hamilton East,916
180300,Melville South,917
180400,Deanwell,918
180500,Bader,919
180600,Hillcrest West (Hamilton City),920
180700,Hillcrest East (Hamilton City),921
180800,Silverdale (Hamilton City),922
180900,Glenview,923
181000,Resthill,924
181100,Fitzroy,925
181200,Riverlea,926
181300,Peacockes,927
181400,Te Pahu,928
181500,Ngahinapouri,929
181600,Lake Cameron,930
181700,Lake Ngaroto,931
181800,Kaipaki,932
181900,Pirongia,933
182000,Hautapu Rural,934
182100,Pokuru,935
182200,Te Rahu,936
182300,Fencourt,937
182400,Hautapu,938
182500,Karapiro,939
182600,Cambridge North,940
182700,Cambridge West,941
182800,Cambridge East,942
182900,Cambridge Park-River Garden,943
183000,Oaklands-St Kilda,944
183100,Pukerimu,945
183200,Cambridge Central,946
183300,Te Awamutu North,947
183400,Te Awamutu West,948
183500,Leamington West,949
183600,Goodfellow Park,950
183700,Leamington South,951
183800,Leamington Central,952
183900,Leamington East,953
184000,Te Awamutu Stadium,954
184100,Te Awamutu Central,955
184200,Pekerau,956
184300,Fraser Street,957
184400,Sherwin Park,958
184500,St Leger,959
184600,Rotoorangi,960
184700,Tokanui,961
184800,Kihikihi Central,962
184900,Maungatautari,963
185000,Rotongata,964
185100,Inlets Otorohanga District,965
185200,Pirongia Forest,966
185300,Honikiwi,967
185400,Te Kawa,968
185500,Otorohanga,969
185600,Maihiihi,970
185700,Puniu,971
185800,Tirau,972
185900,Putararu Rural,973
186000,Putararu,974
186100,Kinleith,975
186200,Paraonui,976
186300,Parkdale,977
186400,Matarawa,978
186500,Stanley Park,979
186600,Strathmore (South Waikato District),980
186700,Tokoroa Central,981
186800,Moananui,982
186900,Inlet Waitomo District,983
187000,Herangi,984
187100,Hangatiki,985
187200,Aria,986
187300,Te Kuiti West,987
187400,Te Kuiti East,988
187500,Waipa Valley,989
187700,Marotiri,990
187800,Ohakuri,991
187900,Lake Taupo Bays,992
188000,Mapara,993
188100,Inland water Lake Taupo,994
188200,Wairakei-Broadlands,995
188300,Acacia Bay,996
188400,Brentwood (Taupo District),997
188500,Nukuhau-Rangatira Park,998
188600,Taupo Central West,999
188700,Tauhara,1000";

            var a = content.Split("\r\n");

            return a;
        }

        private string GetLongContent()
        {
            string fakeFileContents = @"Code,Description,SortOrder
9999,Total - New Zealand by Regional Council/SA2,1
999999,Total - New Zealand by Territorial Authority/SA2,2
DHB9999,Total - New Zealand by District Health Board,3
01,Northland Region,4
02,Auckland Region,5
03,Waikato Region,6
04,Bay of Plenty Region,7
05,Gisborne Region,8
06,Hawke's Bay Region,9
07,Taranaki Region,10
08,Manawatu-Wanganui Region,11
09,Wellington Region,12
16,Tasman Region,13
17,Nelson Region,14
18,Marlborough Region,15
12,West Coast Region,16
13,Canterbury Region,17
14,Otago Region,18
15,Southland Region,19
77,Total - Regional Council Areas,20
99,Area Outside Region,21
001,Far North District,22
002,Whangarei District,23
003,Kaipara District,24
076,Auckland,25
CMB07601,Rodney Local Board Area,26
CMB07602,Hibiscus and Bays Local Board Area,27
CMB07603,Upper Harbour Local Board Area,28
CMB07604,Kaipatiki Local Board Area,29
CMB07605,Devonport-Takapuna Local Board Area,30
CMB07606,Henderson-Massey Local Board Area,31
CMB07607,Waitakere Ranges Local Board Area,32
CMB07608,Great Barrier Local Board Area,33
CMB07609,Waiheke Local Board Area,34
CMB07610,Waitemata Local Board Area,35
CMB07611,Whau Local Board Area,36
CMB07612,Albert-Eden Local Board Area,37
CMB07613,Puketapapa Local Board Area,38
CMB07614,Orakei Local Board Area,39
CMB07615,Maungakiekie-Tamaki Local Board Area,40
CMB07616,Howick Local Board Area,41
CMB07617,Mangere-Otahuhu Local Board Area,42
CMB07618,Otara-Papatoetoe Local Board Area,43
CMB07619,Manurewa Local Board Area,44
CMB07620,Papakura Local Board Area,45
CMB07621,Franklin Local Board Area,46
011,Thames-Coromandel District,47
012,Hauraki District,48
013,Waikato District,49
015,Matamata-Piako District,50
016,Hamilton City,51
017,Waipa District,52
018,Otorohanga District,53
019,South Waikato District,54
020,Waitomo District,55
021,Taupo District,56
022,Western Bay of Plenty District,57
023,Tauranga City,58
024,Rotorua District,59
025,Whakatane District,60
026,Kawerau District,61
027,Opotiki District,62
028,Gisborne District,63
029,Wairoa District,64
030,Hastings District,65
031,Napier City,66
032,Central Hawke's Bay District,67
033,New Plymouth District,68
034,Stratford District,69
035,South Taranaki District,70
036,Ruapehu District,71
037,Whanganui District,72
038,Rangitikei District,73
039,Manawatu District,74
040,Palmerston North City,75
041,Tararua District,76
042,Horowhenua District,77
043,Kapiti Coast District,78
044,Porirua City,79
045,Upper Hutt City,80
046,Lower Hutt City,81
047,Wellington City,82
048,Masterton District,83
049,Carterton District,84
050,South Wairarapa District,85
051,Tasman District,86
052,Nelson City,87
053,Marlborough District,88
054,Kaikoura District,89
055,Buller District,90
056,Grey District,91
057,Westland District,92
058,Hurunui District,93
059,Waimakariri District,94
060,Christchurch City,95
062,Selwyn District,96
063,Ashburton District,97
064,Timaru District,98
065,Mackenzie District,99
066,Waimate District,100
067,Chatham Islands Territory,101
068,Waitaki District,102
069,Central Otago District,103
070,Queenstown-Lakes District,104
071,Dunedin City,105
072,Clutha District,106
073,Southland District,107
074,Gore District,108
075,Invercargill City,109
777,Total - Territorial Authority areas,110
999,Area Outside Territorial Authority,111
100100,North Cape,112
100200,Rangaunu Harbour,113
100300,Inlets Far North District,114
100400,Karikari Peninsula,115
100500,Tangonge,116
100600,Ahipara,117
100700,Kaitaia East,118
100800,Kaitaia West,119
100900,Rangitihi,120
101000,Oruru-Parapara,121
101100,Taumarumaru,122
101200,Herekino-Takahue,123
101300,Peria,124
101400,Taemaro-Oruaiti,125
101500,Whakapaku,126
101600,Hokianga North,127
101700,Kohukohu-Broadwood,128
101800,Whakarara,129
101900,Kaeo,130
102000,Omahuta Forest-Horeke,131
102100,Hokianga South,132
102200,Lake Manuwai-Kapiro,133
102300,Okaihau,134
102400,Rangitane-Purerua,135
102500,Waipapa,136
102600,Puketotara,137
102700,Waima Forest,138
102800,Riverview,139
102900,Waipoua Forest,140
103000,Kerikeri Central,141
103100,Kerikeri South,142
103200,Ohaeawai-Waimate North,143
103300,Puketona-Waitangi,144
103400,Ngapuhi,145
103500,Kaikohe,146
103600,Pakaraka,147
103700,Haruru,148
103800,Russell,149
103900,Paihia,150
104000,Mataraua Forest,151
104100,Matawaia-Taumarere,152
104200,Russell Peninsula,153
104300,Opua (Far North District),154
104400,Russell Forest-Rawhiti,155
104500,Moerewa,156
104600,Kawakawa,157
104700,Maromaku,158
104800,Mangakahia-Hukerenui,159
104900,Whangaruru,160
105000,Inlets other Whangarei District,161
105100,Matarau,162
105200,Hikurangi,163
105300,Kauri,164
105400,Maungatapere,165
105500,Kamo West,166
105600,Matapouri-Tutukaka,167
105700,Kiripaka,168
105800,Kamo East,169
105900,Granfield Reserve,170
106000,Kamo Central,171
106100,Whau Valley,172
106200,Tikipunga North,173
106300,Pukenui,174
106400,Otangarei,175
106500,Tikipunga South,176
106600,Kensington (Whangarei District),177
106700,Abbey Caves-Glenbervie,178
106800,Mairtown,179
106900,Maunu-Horahora,180
107000,Woodhill-Vinetown,181
107100,Whangarei Central,182
107200,Riverside,183
107300,Raumanga,184
107400,Tarewa,185
107500,Morningside (Whangarei District),186
107600,Ngunguru,187
107700,Otaika-Portland,188
107800,Oakleigh-Mangapai,189
107900,Port-Limeburners,190
108000,Pataua,191
108100,Onerahi Park,192
108200,Sherwood Rise,193
108300,Onerahi,194
108400,Inlet Whangarei Harbour,195
108500,Parua Bay,196
108600,Bream Bay,197
108700,Marsden Bay,198
108800,Ruakaka,199
108900,Bream Head,200
109000,Waipu,201
109100,Kaipara Coastal,202
109200,Maungaru,203
109300,Dargaville,204
109400,Ruawai-Matakohe,205
109500,Otamatea (Kaipara District),206
109600,Maungaturoto,207
109700,Kaiwaka,208
109800,Mangawhai Rural,209
109900,Mangawhai Heads,210
110000,Inlet Mangawhai Harbour,211
110100,Mangawhai,212
258200,Oceanic Northland Region,213
258300,Inlet Doubtless Bay,214
258400,Inlet Kaipara Harbour North,215
110200,Okahukura Peninsula,216
110300,Inlet Kaipara Harbour South,217
110400,Cape Rodney,218
110500,Wellsford,219
110600,Oceanic Auckland Region East,220
110700,South Head,221
110800,Kaipara Hills,222
110900,Dome Valley-Matakana,223
111000,Oceanic Auckland Region West,224
111100,Warkworth West,225
111200,Puhoi Valley,226
111300,Warkworth East,227
111400,Sandspit,228
111500,Tawharanui Peninsula,229
111600,Te Kuru,230
111700,Snells Beach,231
111800,Barrier Islands,232
111900,Algies Bay-Scotts Landing,233
112000,Inlets other Auckland,234
112100,Wainui-Waiwera,235
112200,Parakai,236
112300,Helensville Rural,237
112400,Helensville,238
112500,Waitoki,239
112600,Waikoukou Valley,240
112700,Orewa North,241
112800,Hatfields Beach,242
112900,Orewa South,243
113000,Orewa Central,244
113100,Millwater North,245
113200,Waipatukahu,246
113300,Millwater South,247
113400,Dairy Flat North,248
113500,Dairy Flat West,249
113600,Kingsway,250
113700,Riverhead Forest,251
113800,Silverdale Central (Auckland),252
113900,Red Beach West,253
114000,Red Beach East,254
114100,Silverdale South (Auckland),255
114200,Waimauku,256
114300,Gulf Islands,257
114400,Vipond,258
114500,Muriwai,259
114600,Stanmore Bay West,260
114700,Kumeu Rural West,261
114800,Okura Bush,262
114900,Whangaparoa Central,263
115000,Kumeu-Huapai,264
115100,Stanmore Bay East,265
115200,Manly West,266
115300,Muriwai Valley-Bethells Beach,267
115400,Tindalls-Matakatia,268
115500,Coatesville,269
115600,Dairy Flat South,270
115700,Gulf Harbour North,271
115800,Manly East,272
115900,Riverhead,273
116000,Army Bay,274
116100,Kumeu Rural East,275
116200,Gulf Harbour South,276
116300,Paremoremo West,277
116400,Taupaki,278
116500,Long Bay,279
116600,Paremoremo East,280
116700,Albany Heights,281
116800,Awaruku,282
116900,Fairview Heights,283
117000,Whenuapai,284
117100,Waitakere Ranges North,285
117200,Torbay,286
117300,Albany Central,287
117400,Albany West,288
117500,Glamorgan,289
117600,Oteha East,290
117700,Northcross,291
117800,Oteha West,292
117900,Waiake,293
118000,Waitakere,294
118100,Albany South,295
118200,Browns Bay South West,296
118300,Browns Bay Central,297
118400,Schnapper Rock,298
118500,Pinehill,299
118600,North Harbour,300
118700,Browns Bay South East,301
118800,Westgate Central,302
118900,Greenhithe West,303
119000,Rothesay Bay,304
119100,Greenhithe East,305
119200,Hobsonville,306
119300,Inlet Waitemata Harbour,307
119400,Greenhithe South,308
119500,Murrays Bay West,309
119600,Westgate South,310
119700,Windsor Park,311
119800,West Harbour West,312
119900,Unsworth Heights West,313
120000,Murrays Bay East,314
120100,Birdwood West,315
120200,Hobsonville Point,316
120300,West Harbour Clearwater Cove,317
120400,Unsworth Heights East,318
120500,Mairangi Bay North,319
120600,Massey Central,320
120700,West Harbour Luckens Point,321
120800,Mairangi Bay South,322
120900,Massey Royal Road West,323
121000,Bayview East,324
121100,Swanson Rural,325
121200,Sunnynook South,326
121300,Sunnynook North,327
121400,Bayview West,328
121500,Totara Vale North,329
121600,Royal Heights North,330
121700,Massey West,331
121800,Beach Haven West,332
121900,Totara Vale South,333
122000,Swanson,334
122100,Bayview South,335
122200,Campbells Bay,336
122300,Royal Heights South,337
122400,Beach Haven East,338
122500,Massey East,339
122600,Forrest Hill North,340
122700,Glenfield North,341
122800,Birkdale North,342
122900,Massey South,343
123000,Ranui North,344
123100,Glenfield West,345
123200,Te Atatu Peninsula North West,346
123300,Glenfield South West,347
123400,Forrest Hill West,348
123500,Wairau Valley,349
123600,Forrest Hill East,350
123700,Castor Bay,351
123800,Ranui Domain,352
123900,Beach Haven South,353
124000,Glenfield Central,354
124100,Piha,355
124200,Ranui South West,356
124300,Henderson Larnoch,357
124400,Birkdale South,358
124500,Te Atatu Peninsula Central,359
124600,Glenfield East,360
124700,Henderson Valley,361
124800,Te Atatu Peninsula West,362
124900,Henderson Lincoln West,363
125000,Ranui South East,364
125100,Henderson Lincoln East,365
125200,Birkenhead West,366
125300,Te Atatu Peninsula East,367
125400,Milford West,368
125500,Westlake,369
125600,Hillcrest North (Auckland),370
125700,Henderson Lincoln South,371
125800,Hillcrest West (Auckland),372
125900,Milford Central,373
126000,Summerland South,374
126100,Summerland North,375
126200,Hillcrest East (Auckland),376
126300,Chatswood,377
126400,Te Atatu South-Edmonton,378
126500,Birkenhead North,379
126600,Takapuna West,380
126700,Henderson North,381
126800,Takapuna Central,382
126900,Western Heights (Auckland),383
127000,Te Atatu South-Central,384
127100,Northcote Central (Auckland),385
127200,Akoranga,386
127300,Northcote South (Auckland),387
127400,Birkenhead South,388
127500,Henderson Central,389
127600,Waiatarua,390
127700,Te Atatu South-McLeod North,391
127800,Henderson Valley Park,392
127900,Henderson North East,393
128000,Waitakere Ranges South,394
128100,Northcote Tuff Crater,395
128200,Northcote Point (Auckland),396
128300,McLaren Park,397
128400,Henderson East,398
128500,Te Atatu South-McLeod South,399
128600,Takapuna South,400
128700,Rosebank Peninsula,401
128800,Hauraki,402
128900,Glendene North,403
129000,Sunnyvale West-Parrs Park,404
129100,Sunnyvale East,405
129200,Point Chevalier West,406
129300,Belmont (Auckland),407
129400,Bayswater,408
129500,Glendene South,409
129600,Glen Eden West,410
129700,Herne Bay,411
129800,Westmere North,412
129900,Point Chevalier East,413
130000,Oratia,414
130100,Glen Eden Rosier,415
130200,Saint Marys Bay,416
130300,Westmere South-Western Springs,417
130400,Ponsonby West,418
130500,Kelston North,419
130600,Avondale Rosebank,420
130700,Narrow Neck,421
130800,Glen Eden North,422
130900,Glen Eden Woodglen,423
131000,Kelston South,424
131100,Waterview,425
131200,Ponsonby East,426
131300,Wynyard-Viaduct,427
131400,Stanley Point,428
131500,Glen Eden Central,429
131600,Grey Lynn North,430
131700,Avondale West (Auckland),431
131800,Freemans Bay,432
131900,Mount Albert West,433
132000,Avondale North (Auckland),434
132100,Grey Lynn West,435
132200,Mount Albert North,436
132300,New Lynn North,437
132400,Victoria Park,438
132500,Konini,439
132600,New Lynn North West,440
132700,Hobson Ridge North,441
132800,Grey Lynn Central,442
132900,Fruitvale,443
133000,Devonport,444
133100,Avondale Central (Auckland),445
133200,Queen Street,446
133300,Quay Street-Customs Street,447
133400,Hobson Ridge Central,448
133500,Grey Lynn East,449
133600,Morningside (Auckland),450
133700,Shortland Street,451
133800,Hobson Ridge South,452
133900,New Lynn Central,453
134000,Cheltenham,454
134100,Queen Street South West,455
134200,Mount Albert Central,456
134300,Karangahape,457
134400,Kingsland,458
134500,Anzac Avenue,459
134600,Waima-Woodlands Park,460
134700,Kaurilands,461
134800,Auckland-University,462
134900,New Lynn Seabrook,463
135000,Avondale South (Auckland),464
135100,Symonds Street North West,465
135200,Mount Albert South,466
135300,Symonds Street West,467
135400,West Lynn,468
135500,Owairaka West,469
135600,New Lynn Central South,470
135700,The Strand,471
135800,St Lukes,472
135900,Symonds Street East,473
136000,Eden Terrace,474
136100,Grafton,475
136200,Eden Park,476
136300,Sandringham North,477
136400,Parnell West,478
136500,New Lynn South,479
136600,New Windsor North,480
136700,Titirangi South,481
136800,Owairaka East,482
136900,Eden Valley,483
137000,Sandringham Central,484
137100,Glenavon,485
137200,Blockhouse Bay North,486
137300,Green Bay North,487
137400,Parnell East,488
137500,Laingholm,489
137600,Sandringham West,490
137700,Mount Eden North East,491
137800,Mount Eden North,492
137900,Wesley West,493
138000,Green Bay South,494
138100,Balmoral,495
138200,Sandringham East,496
138300,New Windsor South,497
138400,Mount Eden West,498
138500,Newmarket,499
138600,Blockhouse Bay North East,500
138700,Epsom North,501
138800,Wesley South,502
138900,Blockhouse Bay South,503
139000,Mount Eden East,504
139100,Wesley East,505
139200,Orakei West,506
139300,Maungawhau,507
139400,Remuera West,508
139500,Blockhouse Bay East,509
139600,Lynfield North,510
139700,Mount Roskill North,511
139800,Mount Roskill White Swan,512
139900,Mount Eden South,513
140000,Remuera Waitaramoa,514
140100,Orakei East,515
140200,Epsom Central-North,516
140300,Mount St John,517
140400,Mount Roskill Central North,518
140500,Remuera North,519
140600,Three Kings West,520
140700,Mount Roskill Central South,521
140800,Epsom Central-South,522
140900,Lynfield South,523
141000,Remuera South,524
141100,Mission Bay,525
141200,Three Kings East,526
141300,Mount Roskill South,527
141400,Inlet Manukau Harbour,528
141500,Epsom East,529
141600,Remuera Waiata,530
141700,Epsom South,531
141800,Kohimarama,532
141900,Meadowbank West,533
142000,Hillsborough North (Auckland),534
142100,Hillsborough West (Auckland),535
142200,Greenlane North,536
142300,Waikowhai North,537
142400,Remuera East,538
142500,Waikowhai South,539
142600,Royal Oak West (Auckland),540
142700,Saint Heliers West,541
142800,One Tree Hill,542
142900,Hillsborough East (Auckland),543
143000,Saint Heliers North,544
143100,Remuera Abbotts Park,545
143200,Meadowbank East,546
143300,Ellerslie Central,547
143400,Royal Oak East (Auckland),548
143500,Saint Heliers South,549
143600,Hillsborough South (Auckland),550
143700,Saint Johns West,551
143800,Greenlane South,552
143900,Remuera Waiatarua,553
144000,Glendowie North,554
144100,Onehunga West,555
144200,Ellerslie West,556
144300,Onehunga North,557
144400,Ellerslie East,558
144500,Saint Johns East,559
144600,Oneroa West,560
144700,Glen Innes West,561
144800,Ellerslie South,562
144900,Stonefields West,563
145000,Onehunga Central,564
145100,Glendowie South,565
145200,Oranga,566
145300,Mount Wellington North West,567
145400,Panmure Glen Innes Industrial,568
145500,Onehunga-Te Papapa Industrial,569
145600,Te Papapa,570
145700,Stonefields East,571
145800,Glen Innes East-Wai O Taiki Bay,572
145900,Penrose,573
146000,Mount Wellington North East,574
146100,Mangere Bridge Ambury,575
146200,Mount Wellington Ferndale,576
146300,Point England,577
146400,Bucklands Beach North,578
146500,Mount Wellington East,579
146600,Mount Wellington West,580
146700,Panmure West,581
146800,Mangere Bridge,582
146900,Oneroa East-Palm Beach,583
147000,Tamaki,584
147100,Panmure East,585
147200,Mount Wellington Central,586
147300,Bays Waiheke Island,587
147400,Sylvia Park,588
147500,Mangere Mountain View,589
147600,Surfdale,590
147700,Mount Wellington Industrial,591
147800,Half Moon Bay West,592
147900,Auckland Airport,593
148000,Eastern Beach,594
148100,Sunnyhills West-Pakuranga North,595
148200,Favona North,596
148300,Bucklands Beach Central,597
148400,Farm Cove,598
148500,Mangere North,599
148600,Favona West,600
148700,Ostend,601
148800,Half Moon Bay North East,602
148900,Mangere West,603
149000,Pakuranga West,604
149100,Mount Wellington South West,605
149200,Sunnyhills East,606
149300,Half Moon Bay South East,607
149400,Favona East,608
149500,Mount Wellington South East,609
149600,Otahuhu Industrial,610
149700,Bucklands Beach South,611
149800,Pakuranga Central,612
149900,Awhitu,613
150000,Harania North,614
150100,Otahuhu Central,615
150200,Mangere Central,616
150300,Otahuhu North,617
150400,Howick West,618
150500,Pakuranga Heights North West,619
150600,Mellons Bay,620
150700,Pakuranga Heights East,621
150800,Sutton Park,622
150900,Otahuhu East,623
151000,Mangere South,624
151100,Onetangi,625
151200,Harania South,626
151300,Highland Park (Auckland),627
151400,Otahuhu South West,628
151500,Pakuranga Heights South West,629
151600,Mangere Mascot,630
151700,Waiheke East,631
151800,Massey Road West,632
151900,Otahuhu South,633
152000,Howick Central,634
152100,Massey Road North,635
152200,Botany Downs West,636
152300,East Tamaki,637
152400,Howick East,638
152500,Burswood,639
152600,Massey Road South,640
152700,Middlemore,641
152800,Mangere South East,642
152900,Botany Downs East,643
153000,Grange,644
153100,Golflands,645
153200,Aorere North,646
153300,Mangere East,647
153400,Cockle Bay,648
153500,Otara West,649
153600,Aorere Central,650
153700,Northpark North,651
153800,Somerville,652
153900,Northpark South,653
154000,Papatoetoe North,654
154100,Dingwall,655
154200,Aorere South,656
154300,Shelly Park,657
154400,Otara Central,658
154500,Huntington Park,659
154600,Botany Central,660
154700,Botany North,661
154800,Papatoetoe West,662
154900,Papatoetoe Central,663
155000,Sunkist Bay,664
155100,Otara East,665
155200,Redcastle,666
155300,Botany East,667
155400,Botany South,668
155500,Manukau Central,669
155600,Papatoetoe East,670
155700,Otara South,671
155800,Ferguson,672
155900,Papatoetoe South West,673
156000,Botany Junction,674
156100,Dannemora North,675
156200,Papatoetoe South,676
156300,East Tamaki Heights,677
156400,Te Puru,678
156500,Puhinui North,679
156600,Dannemora South,680
156700,Puhinui South,681
156800,Rongomai West,682
156900,Baverstock,683
157000,Puhinui East,684
157100,Turanga,685
157200,Rongomai East,686
157300,Ormiston North,687
157400,Maraetai,688
157500,Chapel Downs,689
157600,Wiri West,690
157700,Clover Park North,691
157800,Mission Heights North,692
157900,Clover Park South,693
158000,Donegal Park,694
158100,Ormiston South,695
158200,Mission Heights South,696
158300,Hilltop (Auckland),697
158400,Wiri East,698
158500,Goodwood Heights,699
158600,Ormiston East,700
158700,Clendon Park North,701
158800,Burbank,702
158900,Tuscany Heights,703
159000,Totara Heights,704
159100,Homai East,705
159200,Homai West,706
159300,The Gardens (Auckland),707
159400,Clarks Beach,708
159500,Clendon Park West,709
159600,Hillpark North,710
159700,Clendon Park East,711
159800,Karaka Creek,712
159900,Rowandale West,713
160000,Rowandale East,714
160100,Manurewa Central,715
160200,Weymouth North,716
160300,Hillpark South,717
160400,Alfriston,718
160500,Weymouth East,719
160600,Leabank,720
160700,Weymouth South,721
160800,Manurewa East,722
160900,Manurewa South,723
161000,Randwick Park East,724
161100,Wattle Downs West,725
161200,Wattle Downs North,726
161300,Randwick Park West,727
161400,Kingseat-Karaka,728
161500,Wattle Downs East,729
161600,Takanini North,730
161700,Takanini Industrial,731
161800,Conifer Grove West,732
161900,Conifer Grove East,733
162000,Takanini West,734
162100,Takanini South,735
162200,Takanini Central,736
162300,Ardmore,737
162400,Glenbrook,738
162500,Hingaia,739
162600,Takanini South East,740
162700,Kawakawa Bay-Orere,741
162800,Papakura West,742
162900,Clevedon,743
163000,Papakura North,744
163100,Pahurehure,745
163200,Papakura Central,746
163300,Papakura North East,747
163400,Karaka Lakes,748
163500,Papakura Kelvin,749
163600,Papakura Massey Park,750
163700,Rosehill,751
163800,Patumahoe,752
163900,Papakura East,753
164000,Opaheke,754
164100,Papakura Industrial,755
164200,Red Hill,756
164300,Drury,757
164400,Tamakae,758
164500,Hamilton Estate,759
164600,Drury Rural,760
164700,Waiuku Central,761
164800,Waiuku East,762
164900,Kendallvale,763
165000,Puni,764
165100,Ramarama,765
165200,Hunua,766
165300,Pukekohe North West,767
165400,Anselmi Ridge,768
165500,Pukekohe West,769
165600,Cape Hill,770
165700,Rosa Birch Park,771
165800,Rooseville Park,772
165900,Cloverlea (Auckland),773
166000,Pukekohe Central,774
166100,Pukekohe Hospital,775
166200,Buckland,776
166300,Bombay Hills,777
166400,Ararimu,778
166500,Colville,779
166600,Islands Thames-Coromandel District,780
166700,Coromandel,781
166800,Inlets Thames-Coromandel District,782
166900,Mercury Bay North,783
167000,Whitianga North,784
167100,Whitianga South,785
167200,Thames Coast,786
167300,Cooks Beach-Ferry Landing,787
167400,Mercury Bay South,788
167500,Kauaeranga,789
167600,Thames North,790
167700,Thames Central,791
167800,Thames South,792
167900,Hikuai,793
168000,Totora-Kopu,794
168100,Tairua,795
168200,Pauanui,796
168300,Matatoki-Puriri,797
168400,Whangamata Rural,798
168500,Whangamata,799
168600,Miranda-Pukorokoro,800
168700,Hauraki Plains North,801
168800,Hauraki Plains East,802
168900,Ngatea,803
169000,Hauraki Plains South,804
169100,Paeroa Rural,805
169200,Paeroa,806
169300,Waihi Rural,807
169400,Waihi North,808
169500,Waihi East,809
169600,Waihi South,810
169700,Aka Aka,811
169800,Mangatangi,812
169900,Tuakau Rural,813
170000,Tuakau North,814
170100,Onewhero,815
170200,Pokeno Rural,816
170300,Tuakau South,817
170400,Port Waikato-Waikaretu,818
170500,Pokeno,819
170600,Pukekawa,820
170700,Maramarua,821
170800,Rangiriri,822
170900,Te Akau,823
171000,Inlets Waikato District,824
171100,Te Kauwhata,825
171200,Huntly Rural,826
171300,Waerenga,827
171400,Huntly West,828
171500,Huntly East,829
171600,Raglan,830
171700,Whitikahu,831
171800,Te Uku,832
171900,Taupiri-Lake Kainui,833
172000,Ngaruawahia North,834
172100,Ngaruawahia Central,835
172200,Ngaruawahia South,836
172300,Kainui-Gordonton,837
172400,Te Kowhai,838
172500,Whatawhata West,839
172600,Horotiu,840
172700,Horsham Downs,841
172800,Whatawhata East,842
172900,Rotokauri,843
173000,Hamilton Park,844
173100,Eureka-Tauwhare,845
173200,Tamahere North,846
173300,Pukemoremore,847
173400,Tamahere South,848
173500,Tahuna-Mangateparu,849
173600,Mangaiti,850
173700,Tatuanui,851
173800,Tahuroa,852
173900,Morrinsville East,853
174000,Morrinsville West,854
174100,Te Aroha East,855
174200,Te Aroha West,856
174300,Waihou-Manawaru,857
174400,Waitoa-Ngarua,858
174500,Richmond Downs-Wardville,859
174600,Waharoa-Peria,860
174700,Okauia,861
174800,Hinuera,862
174900,Matamata North,863
175000,Matamata South,864
175100,Te Poi,865
175200,Te Rapa North,866
175300,Flagstaff North,867
175400,Rotokauri-Waiwhakareke,868
175500,Flagstaff South,869
175600,Rototuna North,870
175700,Pukete West,871
175800,Flagstaff East,872
175900,Rototuna Central,873
176000,Pukete East,874
176100,Te Manatu,875
176200,Rototuna South,876
176300,Te Rapa South,877
176400,Saint Andrews West,878
176500,Saint Andrews East,879
176600,Queenwood (Hamilton City),880
176700,St James,881
176800,Crawshaw,882
176900,Huntington,883
177000,Western Heights (Hamilton City),884
177100,Nawton West,885
177200,Nawton East,886
177300,Chartwell,887
177400,Forest Lake (Hamilton City),888
177500,Chedworth,889
177600,Beerescourt,890
177700,Miropiko,891
177800,Porritt,892
177900,Dinsdale North,893
178000,Maeroa,894
178100,Dinsdale South,895
178200,Fairfield (Hamilton City),896
178300,Whitiora,897
178400,Enderley North,898
178500,Fairview Downs,899
178600,Temple View,900
178700,Swarbrick,901
178800,Kahikatea,902
178900,Frankton Junction,903
179000,Kirikiriroa,904
179100,Enderley South,905
179200,Ruakura,906
179300,Claudelands,907
179400,Hamilton Central,908
179500,Hamilton Lake,909
179600,Peachgrove,910
179700,Hamilton East Village,911
179800,Hamilton West,912
179900,Greensboro,913
180000,Hamilton East Cook,914
180100,Melville North,915
180200,Hamilton East,916
180300,Melville South,917
180400,Deanwell,918
180500,Bader,919
180600,Hillcrest West (Hamilton City),920
180700,Hillcrest East (Hamilton City),921
180800,Silverdale (Hamilton City),922
180900,Glenview,923
181000,Resthill,924
181100,Fitzroy,925
181200,Riverlea,926
181300,Peacockes,927
181400,Te Pahu,928
181500,Ngahinapouri,929
181600,Lake Cameron,930
181700,Lake Ngaroto,931
181800,Kaipaki,932
181900,Pirongia,933
182000,Hautapu Rural,934
182100,Pokuru,935
182200,Te Rahu,936
182300,Fencourt,937
182400,Hautapu,938
182500,Karapiro,939
182600,Cambridge North,940
182700,Cambridge West,941
182800,Cambridge East,942
182900,Cambridge Park-River Garden,943
183000,Oaklands-St Kilda,944
183100,Pukerimu,945
183200,Cambridge Central,946
183300,Te Awamutu North,947
183400,Te Awamutu West,948
183500,Leamington West,949
183600,Goodfellow Park,950
183700,Leamington South,951
183800,Leamington Central,952
183900,Leamington East,953
184000,Te Awamutu Stadium,954
184100,Te Awamutu Central,955
184200,Pekerau,956
184300,Fraser Street,957
184400,Sherwin Park,958
184500,St Leger,959
184600,Rotoorangi,960
184700,Tokanui,961
184800,Kihikihi Central,962
184900,Maungatautari,963
185000,Rotongata,964
185100,Inlets Otorohanga District,965
185200,Pirongia Forest,966
185300,Honikiwi,967
185400,Te Kawa,968
185500,Otorohanga,969
185600,Maihiihi,970
185700,Puniu,971
185800,Tirau,972
185900,Putararu Rural,973
186000,Putararu,974
186100,Kinleith,975
186200,Paraonui,976
186300,Parkdale,977
186400,Matarawa,978
186500,Stanley Park,979
186600,Strathmore (South Waikato District),980
186700,Tokoroa Central,981
186800,Moananui,982
186900,Inlet Waitomo District,983
187000,Herangi,984
187100,Hangatiki,985
187200,Aria,986
187300,Te Kuiti West,987
187400,Te Kuiti East,988
187500,Waipa Valley,989
187700,Marotiri,990
187800,Ohakuri,991
187900,Lake Taupo Bays,992
188000,Mapara,993
188100,Inland water Lake Taupo,994
188200,Wairakei-Broadlands,995
188300,Acacia Bay,996
188400,Brentwood (Taupo District),997
188500,Nukuhau-Rangatira Park,998
188600,Taupo Central West,999
188700,Tauhara,1000
188800,Taupo Central East,1001
188900,Mountview,1002
189000,Bird Area,1003
189100,Hilltop (Taupo District),1004
189200,Waipahihi,1005
189300,Richmond Heights,1006
189400,Wharewaka,1007
189500,Kaimanawa,1008
189600,Waitahanui,1009
189700,Turangi,1010
197800,Arahiwi,1011
201600,Ngakuru,1012
201800,Golden Springs,1013
258500,Oceanic Waikato Region East,1014
258600,Inlets Waikato Region,1015
258700,Oceanic Waikato Region West,1016
189800,Rangataiki,1017
190100,Waiau,1018
190200,Waihi Beach-Bowentown,1019
190300,Tahawai,1020
190400,Athenree,1021
190500,Aongatete,1022
190600,Katikati,1023
190700,Inlet Tauranga Harbour North,1024
190800,Matakana Island,1025
190900,Pahoia,1026
191000,Omokoroa,1027
191100,Omokoroa Rural,1028
191200,Te Puna,1029
191300,Minden,1030
191400,Kaimai,1031
191500,Kopurererua,1032
191600,Kaitemako (Western Bay of Plenty District),1033
191700,Waiorohi,1034
191800,Otawa,1035
191900,Te Puke West,1036
192000,Rangiuru,1037
192100,Te Puke East,1038
192200,Inlets Maketu,1039
192300,Maketu,1040
192400,Pukehina Beach,1041
192500,Pongakawa,1042
192600,Matua North,1043
192700,Inlet Tauranga Harbour South,1044
192800,Mount Maunganui North,1045
192900,Matua South,1046
193000,Bethlehem North,1047
193100,Bellevue,1048
193200,Otumoetai North,1049
193300,Otumoetai South,1050
193400,Brookfield West,1051
193500,Bethlehem Central,1052
193600,Brookfield East,1053
193700,Mount Maunganui South,1054
193800,Tauranga Central,1055
193900,Mount Maunganui Central,1056
194000,Judea,1057
194100,Te Reti,1058
194200,Bethlehem South,1059
194300,Omanu Beach,1060
194400,Tauranga Hospital,1061
194500,Tauriko,1062
194600,Gate Pa,1063
194700,Greerton South,1064
194800,Tauranga South,1065
194900,Arataki North,1066
195000,Matapihi,1067
195100,Pyes Pa West,1068
195200,Greerton North,1069
195300,Yatton Park,1070
195400,Pyes Pa North,1071
195500,Arataki South,1072
195600,Pyes Pa South,1073
195700,Poike,1074
195800,Te Maunga North,1075
195900,Maungatapu,1076
196000,Hairini,1077
196100,Pyes Pa East,1078
196200,Te Maunga South,1079
196300,Kaitemako (Tauranga City),1080
196400,Ohauiti,1081
196500,Baypark-Kairua,1082
196600,Welcome Bay West,1083
196700,Welcome Bay East,1084
196800,Pacific View,1085
196900,Welcome Bay South,1086
197000,Palm Beach North,1087
197100,Palm Beach South-Gravatt,1088
197200,Papamoa Beach North,1089
197300,Doncaster,1090
197400,Papamoa Beach South,1091
197500,Motiti,1092
197600,Wairakei,1093
197700,Tui Ridge,1094
197900,Ngongotaha Valley,1095
198000,Hamurana,1096
198100,Ngongotaha East,1097
198200,Ngongotaha West,1098
198300,Inland water Lake Rotorua,1099
198400,Ngongotaha South,1100
198500,Selwyn Heights,1101
198600,Pleasant Heights,1102
198700,Rotoiti-Rotoehu,1103
198800,Kawaha,1104
198900,Fairy Springs,1105
199000,Western Heights (Rotorua District),1106
199100,Pukehangi North,1107
199200,Pukehangi South,1108
199300,Mangakakahi Central,1109
199400,Koutu,1110
199500,Mangakakahi West,1111
199600,Sunnybrook,1112
199700,Fordlands,1113
199800,Kuirau,1114
199900,Utuhina,1115
200000,Pomare,1116
200100,Rotorua Central,1117
200200,Hillcrest (Rotorua District),1118
200300,Victoria,1119
200400,Waiohewa,1120
200500,Glenholme North,1121
200600,Springfield South,1122
200700,Springfield North,1123
200800,Glenholme South,1124
200900,Owhata West,1125
201000,Holdens Bay-Rotokawa,1126
201100,Ngapuna,1127
201200,Fenton Park,1128
201300,Tihiotonga-Whakarewarewa,1129
201400,Owhata East,1130
201500,Lynmore,1131
201700,Kaingaroa-Whakarewarewa,1132
201900,Manawahe,1133
202000,Matata-Otakiri,1134
202100,Onepu Spring,1135
202200,Edgecumbe,1136
202300,Thornton-Awakeri,1137
202400,Te Teko Lakes,1138
202500,Coastlands,1139
202600,Whakatane West,1140
202700,Whakatane Central,1141
202800,Trident,1142
202900,Allandale,1143
203000,Mokorua Bush,1144
203100,Wainui,1145
203200,Ohope,1146
203300,Inlet Ohiwa Harbour West,1147
203400,Galatea,1148
203500,Waingarara-Waimana,1149
203600,Murupara,1150
203700,Monika Reserve,1151
203800,Kawerau Industrial,1152
203900,Tarawera Park,1153
204000,Inlet Ohiwa Harbour East,1154
204100,Waiotahi,1155
204200,Cape Runaway,1156
204300,Woodlands,1157
204400,Opotiki,1158
204500,Otara-Tirohanga,1159
204600,Oponae,1160
258800,Oceanic Bay of Plenty Region,1161
258900,Islands Bay of Plenty Region,1162
204700,East Cape,1163
204800,Waipaoa,1164
204900,Ruatoria-Raukumara,1165
205000,Tokomaru,1166
205100,Hangaroa,1167
205200,Wharekaka,1168
205300,Te Arai,1169
205400,Hexton,1170
205500,Lytton,1171
205600,Makaraka-Awapuni,1172
205700,Riverdale,1173
205800,Te Hapara North,1174
205900,Mangapapa North,1175
206000,Elgin,1176
206100,Te Hapara South,1177
206200,Mangapapa East,1178
206300,Mangapapa South,1179
206400,Te Hapara East,1180
206500,Centennial Crescent,1181
206600,Whataupoko East,1182
206700,Whataupoko West,1183
206800,Gisborne Central,1184
206900,Kaiti North,1185
207000,Kaiti South,1186
207100,Outer Kaiti,1187
207200,Tamarau,1188
207300,Wainui-Okitu,1189
259200,Oceanic Gisborne Region,1190
189900,Taharua,1191
207400,Maungataniwha-Raupunga,1192
207500,Inland water Lake Waikaremoana,1193
207600,Frasertown-Ruakituri,1194
207700,Whakaki,1195
207800,Wairoa,1196
207900,Mahia,1197
208000,Puketitiri-Tutira,1198
208100,Sherenden-Crownthorpe,1199
208200,Maraekakaho,1200
208300,Puketapu-Eskdale,1201
208400,Omahu-Pakowhai,1202
208500,Bridge Pa,1203
208600,Twyford,1204
208700,Poukawa,1205
208800,Flaxmere West,1206
208900,Omahu Strip,1207
209000,Lochain Park,1208
209100,Flaxmere Park,1209
209200,Flaxmere South,1210
209300,Irongate,1211
209400,Frimley,1212
209500,Camberley,1213
209600,Clive,1214
209700,St Leonards,1215
209800,Mahora,1216
209900,Raureka,1217
210000,Cornwall Park,1218
210100,Tomoana,1219
210200,Longlands-Pukahu,1220
210300,Raceway Park,1221
210400,Karamu,1222
210500,Hastings Central,1223
210600,Tomoana Crossing,1224
210700,Akina Park,1225
210800,Queens Square,1226
210900,Mayfair,1227
211000,Parkhaven,1228
211100,Parkvale,1229
211200,Mangateretere,1230
211300,Haumoana-Te Awanga,1231
211400,Lucknow,1232
211500,Karanema-St Hill,1233
211600,Havelock North-Central,1234
211700,Brookvale,1235
211800,Iona,1236
211900,Hereworth,1237
212000,Te Mata Hills,1238
212100,Havelock Hills,1239
212200,Kahuranaki,1240
212300,Bay View,1241
212400,Poraiti Hills,1242
212500,Poraiti Flat,1243
212600,Westshore,1244
212700,Inlet Napier City,1245
212800,Onekawa West,1246
212900,Ahuriri,1247
213000,Taradale West,1248
213100,Greenmeadows West,1249
213200,Taradale South,1250
213300,Bluff Hill,1251
213400,Hospital Hill,1252
213500,Tamatea West,1253
213600,Tamatea North,1254
213700,Taradale Central,1255
213800,Tamatea East,1256
213900,Marewa West,1257
214000,Greenmeadows Central,1258
214100,Onekawa Central,1259
214200,Pirimai West,1260
214300,Napier Central,1261
214400,Greenmeadows South,1262
214500,Nelson Park,1263
214600,Bledisloe Park,1264
214700,Pirimai East,1265
214800,Onekawa East,1266
214900,Tareha Reserve,1267
215000,Marewa East,1268
215100,Onekawa South,1269
215200,McLean Park,1270
215300,Maraenui,1271
215400,Meeanee-Awatoto,1272
215500,Mangaonuku,1273
215600,Makaretu,1274
215700,Waipawa,1275
215800,Waipukurau West,1276
215900,Mangarara,1277
216000,Waipukurau East,1278
216100,Taurekaitai,1279
226200,Ngamatea,1280
259400,Oceanic Hawke's Bay Region,1281
259500,Inlet Port Napier,1282
259800,Bare Island,1283
216200,Port Taranaki,1284
216300,Spotswood,1285
216400,Omata,1286
216500,Oakura,1287
216600,Moturoa,1288
216700,Kaitake,1289
216800,Blagdon-Lynmouth,1290
216900,Kawaroa,1291
217000,New Plymouth Central,1292
217100,Marfell,1293
217200,Whalers Gate,1294
217300,Strandon,1295
217400,Westown,1296
217500,Bell Block West,1297
217600,Bell Block East,1298
217700,Waiwhakaiho-Bell Block South,1299
217800,Lower Vogeltown,1300
217900,Hurdon,1301
218000,Frankleigh Park,1302
218100,Merrilands,1303
218200,Ferndale,1304
218300,Welbourn,1305
218400,Fitzroy-Glen Avon,1306
218500,Waitara West,1307
218600,Upper Vogeltown,1308
218700,Highlands Park (New Plymouth District),1309
218800,Paraite,1310
218900,Waitara East,1311
219000,Lepperton-Brixton,1312
219100,Mangorei,1313
219200,Mount Messenger,1314
219300,Mangaoraka,1315
219400,Tikorangi,1316
219500,Everett Park,1317
219600,Inglewood,1318
219700,Tarata,1319
219800,Pembroke,1320
219900,Douglas,1321
220000,Toko,1322
220100,Stratford North,1323
220300,Stratford Central,1324
220400,Stratford South,1325
220500,Cape Egmont,1326
220600,Taungatara,1327
220700,Opunake,1328
220800,Kaponga-Mangatoki,1329
220900,Manaia-Kapuni,1330
221000,Eltham,1331
221100,Okaiawa,1332
221200,Te Roti-Moeroa,1333
221300,Egmont Showgrounds,1334
221400,Normanby-Tawhiti,1335
221500,Ohangai,1336
221600,Turuturu,1337
221700,King Edward Park,1338
221800,Ramanui,1339
221900,Hawera Central,1340
222000,Mangawhio,1341
222100,Manutahi-Waitotora,1342
222200,Patea,1343
259000,Oceanic Taranaki Region,1344
259100,Inlet Port Taranaki,1345
187600,Tiroa,1346
190000,Te More,1347
220200,Whangamomona,1348
222300,Otangiwai-Ohura,1349
222400,Ngapuke,1350
222500,Taumarunui North,1351
222600,Taumarunui Central,1352
222700,Taumarunui East,1353
222800,National Park,1354
222900,Tangiwai,1355
223000,Raetihi,1356
223100,Ohakune,1357
223200,Waiouru,1358
223300,Upper Whanganui,1359
223400,Mowhanau,1360
223500,Brunswick-Papaiti,1361
223600,Castlecliff West,1362
223700,Otamatea (Whanganui District),1363
223800,Castlecliff East,1364
223900,Springvale North,1365
224000,Lower Aramoho,1366
224100,St Johns Hill East,1367
224200,St Johns Hill West,1368
224300,Titoki,1369
224400,Springvale West,1370
224500,Springvale East,1371
224600,Upper Aramoho,1372
224700,Balgownie,1373
224800,Laird Park,1374
224900,Wembley Park,1375
225000,College Estate,1376
225100,Whanganui East-Williams Domain,1377
225200,Gonville West,1378
225300,Gonville North,1379
225400,Cornmarket,1380
225500,Whanganui East-Riverlands,1381
225600,Kaitoke-Fordell,1382
225700,Whanganui Central,1383
225800,Gonville South,1384
225900,Bastia-Durie Hill,1385
226000,Putiki,1386
226100,Mokai Patea,1387
226300,Turakina,1388
226400,Otairi,1389
226500,Taihape,1390
226600,Marton Rural,1391
226700,Marton North,1392
226800,Parewanui,1393
226900,Marton South,1394
227000,Bulls,1395
227100,Kiwitea,1396
227200,Tokorangi,1397
227300,Ohakea-Sanson,1398
227400,Oroua Downs,1399
227500,Awahuri,1400
227600,Pohangina-Apiti,1401
227700,Mount Taylor,1402
227800,Taikorea,1403
227900,Makino,1404
228000,Sandon,1405
228100,Kimbolton North,1406
228200,Warwick,1407
228300,Kimbolton West,1408
228400,Feilding Central,1409
228500,Kimbolton South,1410
228600,Kauwhata,1411
228700,Taonui,1412
228800,Newbury,1413
228900,Palmerston North Airport,1414
229000,Milson North,1415
229100,Cloverlea (Palmerston North City),1416
229200,Tremaine,1417
229300,Milson South,1418
229400,Whakarongo,1419
229500,Westbrook,1420
229600,Takaro North,1421
229700,Pioneer West,1422
229800,Palmerston North Hospital,1423
229900,Highbury East,1424
230000,Park West,1425
230100,Takaro South,1426
230200,Roslyn (Palmerston North City),1427
230300,Kelvin Grove West,1428
230400,Kelvin Grove North,1429
230500,Papaioea North,1430
230600,Palmerston North Central,1431
230700,Awapuni North,1432
230800,Terrace End,1433
230900,Maraetarata,1434
231000,Papaioea South,1435
231100,Royal Oak (Palmerston North City),1436
231200,West End,1437
231300,Awapuni South,1438
231400,Milverton,1439
231500,Ruamahanga,1440
231600,Esplanade,1441
231700,Hokowhitu Central,1442
231800,Hokowhitu East,1443
231900,Turitea,1444
232000,Ruahine,1445
232100,Linton Camp,1446
232200,Ashhurst,1447
232300,Hokowhitu South,1448
232400,Aokautere,1449
232500,Pihauatua,1450
232600,Aokautere Rural,1451
232700,Poutoa,1452
232800,Norsewood,1453
232900,Papatawa,1454
233000,Mangatainoka,1455
233100,Woodville,1456
233200,Dannevirke West,1457
233300,Dannevirke East,1458
233400,Waitahora,1459
233500,Kaitawa,1460
233600,Pahiatua,1461
233700,Nireaha-Eketahuna,1462
233800,Owhanga,1463
234000,Kere Kere,1464
234100,Foxton Beach,1465
234200,Foxton North,1466
234300,Foxton South,1467
234400,Waitarere,1468
234500,Waikawa (Horowhenua District),1469
234600,Miranui,1470
234700,Donnelly Park,1471
234800,Ohau-Manakau,1472
234900,Kawiu South,1473
235000,Makomako,1474
235100,Kawiu North,1475
235200,Levin Central,1476
235300,Tararua,1477
235400,Shannon,1478
235500,Queenwood (Horowhenua District),1479
235600,Playford Park,1480
235700,Fairfield (Horowhenua District),1481
235800,Taitoko,1482
235900,Waiopehu,1483
236000,Makahika,1484
236100,Kimberley,1485
259300,Oceanic Manawatu-Wanganui Region West,1486
259900,Oceanic Manawatu-Wanganui Region East,1487
233900,Mara,1488
236200,Kapiti Island,1489
236300,Otaki Beach,1490
236400,Forest Lakes (Kapiti Coast District),1491
236500,Otaki,1492
236600,Te Horo,1493
236700,Waitohu,1494
236800,Waikanae Beach,1495
236900,Peka Peka,1496
237000,Paraparaumu Beach North,1497
237100,Paraparaumu Beach West,1498
237200,Waikanae Park,1499
237300,Paraparaumu Beach East,1500
237400,Otaihanga,1501
237500,Paraparaumu North,1502
237600,Waikanae West,1503
237700,Otaki Forks,1504
237800,Paraparaumu Central,1505
237900,Maungakotukutuku,1506
238000,Raumati Beach West,1507
238100,Waikanae East,1508
238200,Tararua Forest Park,1509
238300,Raumati Beach East,1510
238400,Paraparaumu East,1511
238500,Raumati South,1512
238600,Paekakariki,1513
238700,Mana Island,1514
238800,Pukerua Bay,1515
238900,Paekakariki Hill,1516
239000,Plimmerton,1517
239100,Titahi Bay North,1518
239200,Titahi Bay South,1519
239300,Elsdon-Takapuwahia,1520
239400,Pauatahanui,1521
239500,Onepoto,1522
239600,Camborne,1523
239700,Inlet Porirua Harbour,1524
239800,Paremata,1525
239900,Porirua Central,1526
240000,Papakowhai,1527
240100,Aotea,1528
240200,Postgate,1529
240300,Ascot Park,1530
240400,Whitby,1531
240500,Porirua East,1532
240600,Endeavour,1533
240700,Cannons Creek North,1534
240800,Waitangirua,1535
240900,Ranui Heights,1536
241000,Cannons Creek East,1537
241100,Cannons Creek South,1538
241200,Akatarawa,1539
241300,Riverstone Terraces,1540
241400,Heretaunga,1541
241500,Birchville-Brown Owl,1542
241600,Poets Block,1543
241700,Brentwood (Upper Hutt City),1544
241800,Silverstream (Upper Hutt City),1545
241900,Elderslea,1546
242000,Trentham North,1547
242100,Totara Park,1548
242200,Trentham South,1549
242300,Mangaroa,1550
242400,Ebdentown,1551
242500,Wallaceville,1552
242600,Maoribank,1553
242700,Te Marua,1554
242800,Pinehaven,1555
242900,Clouston Park,1556
243000,Upper Hutt Central,1557
243100,Belmont Park,1558
243200,Maungaraki,1559
243300,Korokoro,1560
243400,Kelson,1561
243500,Normandale,1562
243600,Belmont (Lower Hutt City),1563
243700,Petone Central,1564
243800,Tirohanga,1565
243900,Manor Park,1566
244000,Alicetown-Melling,1567
244100,Taita North,1568
244200,Boulcott,1569
244300,Hutt Central North,1570
244400,Avalon West,1571
244500,Stokes Valley Central,1572
244600,Taita South,1573
244700,Petone East,1574
244800,Hutt Central South,1575
244900,Stokes Valley North,1576
245000,Petone Esplanade,1577
245100,Epuni West,1578
245200,Avalon East,1579
245300,Woburn,1580
245400,Naenae Central,1581
245500,Waterloo West,1582
245600,Epuni East,1583
245700,Gracefield,1584
245800,Moera,1585
245900,Delaney,1586
246000,Waiwhetu,1587
246100,Waterloo East,1588
246200,Naenae North,1589
246300,Manuka,1590
246400,Naenae South,1591
246500,Towai,1592
246600,Arakura,1593
246700,Eastern Bays,1594
246800,Pencarrow,1595
246900,Wainuiomata West,1596
247000,Glendale,1597
247100,Wainuiomata Central,1598
247200,Eastbourne,1599
247300,Homedale East,1600
247400,Homedale West,1601
247500,Makara-Ohariu,1602
247600,Tawa North,1603
247700,Linden,1604
247800,Tawa South,1605
247900,Tawa Central,1606
248000,Grenada North,1607
248100,Churton Park North,1608
248200,Takapu-Horokiwi,1609
248300,Churton Park South,1610
248400,Johnsonville West,1611
248500,Grenada Village,1612
248600,Johnsonville North,1613
248700,Paparangi,1614
248800,Ngaio North,1615
248900,Johnsonville Central,1616
249000,Broadmeadows,1617
249100,Crofton Downs,1618
249200,Johnsonville South,1619
249300,Khandallah Reserve,1620
249400,Karori Park,1621
249500,Newlands North,1622
249600,Ngaio South,1623
249700,Newlands South,1624
249800,Woodridge,1625
249900,Karori North,1626
250000,Khandallah North,1627
250100,Wilton,1628
250200,Khandallah South,1629
250300,Wadestown,1630
250400,Karori South,1631
250500,Onslow,1632
250600,Karori East,1633
250700,Pipitea-Kaiwharawhara,1634
250800,Northland (Wellington City),1635
250900,Thorndon,1636
251000,Wellington Botanic Gardens,1637
251100,Kelburn,1638
251200,Aro Valley,1639
251300,Wellington University,1640
251400,Wellington Central,1641
251500,Brooklyn North,1642
251600,Dixon Street,1643
251700,Vivian West,1644
251800,Courtenay,1645
251900,Brooklyn East,1646
252000,Mount Cook West,1647
252100,Vivian East,1648
252200,Brooklyn South,1649
252300,Oriental Bay,1650
252400,Mount Cook East,1651
252500,Mount Victoria,1652
252600,Roseneath,1653
252700,Owhiro Bay,1654
252800,Kingston-Mornington-Vogeltown,1655
252900,Newtown North,1656
253000,Newtown West,1657
253100,Hataitai North,1658
253200,Evans Bay,1659
253300,Berhampore,1660
253400,Hataitai South,1661
253500,Maupuia,1662
253600,Newtown South,1663
253700,Kilbirnie Central,1664
253800,Island Bay West,1665
253900,Melrose,1666
254000,Island Bay East,1667
254100,Miramar North,1668
254200,Kilbirnie East,1669
254300,Lyall Bay,1670
254400,Miramar Central,1671
254500,Southgate,1672
254600,Karaka Bay-Worser Bay,1673
254700,Houghton Bay,1674
254800,Miramar East,1675
254900,Miramar South,1676
255000,Rongotai,1677
255100,Strathmore (Wellington City),1678
255200,Seatoun,1679
255300,Kopuaranga,1680
255400,Upper Plain,1681
255500,Opaki,1682
255600,Ngaumutawa,1683
255700,Solway North,1684
255800,Lansdowne West,1685
255900,Masterton Central,1686
256000,Kuripuni,1687
256100,Douglas Park,1688
256200,Solway South,1689
256300,Lansdowne East,1690
256400,Cameron and Soldiers Park,1691
256500,Whareama,1692
256600,Homebush-Te Ore Ore,1693
256700,McJorrow Park,1694
256800,Mount Holdsworth,1695
256900,Carterton North,1696
257000,Kokotau,1697
257100,Carterton South,1698
257200,Gladstone (Carterton District),1699
257300,Tauherenikau,1700
257400,Kahutara,1701
257500,Featherston,1702
257600,Inland water Lake Wairarapa,1703
257700,Greytown,1704
257800,Aorangi Forest,1705
257900,Martinborough,1706
259600,Oceanic Wellington Region,1707
259700,Inlet Wellington Harbour,1708
300200,Oceanic Tasman Region,1709
300300,Golden Bay/Mohua,1710
300400,Inlets Golden Bay,1711
300500,Takaka,1712
300600,Pohara-Abel Tasman,1713
300700,Takaka Hills,1714
300800,Kaiteriteri-Riwaka,1715
300900,Upper Moutere,1716
301000,Lower Moutere,1717
301100,Motueka North,1718
301200,Motueka West,1719
301300,Motueka East,1720
301400,Inlets Motueka,1721
301500,Golden Downs,1722
301600,Moutere Hills,1723
301700,Ruby Bay-Mapua,1724
301800,Murchison-Nelson Lakes,1725
301900,Inlet Waimea West,1726
302000,Islands Tasman District,1727
302100,Waimea West,1728
302200,Appleby,1729
302300,Wakefield,1730
302400,Richmond West (Tasman District),1731
302500,Wakefield Rural,1732
302600,Brightwater,1733
302700,Hope,1734
302800,Richmond Central (Tasman District),1735
302900,Ben Cooper Park,1736
303000,Richmond South (Tasman District),1737
303100,Wilkes Park,1738
303200,Templemore,1739
303300,Easby Park,1740
303400,Fairose,1741
303500,Aniseed Valley,1742
303600,Nelson Rural,1743
303700,Inlets Nelson City,1744
303800,Marybank,1745
303900,Port Nelson,1746
304000,Nelson Airport,1747
304100,Tahunanui,1748
304200,Britannia,1749
304300,Atawhai,1750
304400,Broadgreen-Monaco,1751
304500,Washington,1752
304600,Tahuna Hills,1753
304700,Nelson Central-Trafalgar,1754
304800,The Wood,1755
304900,Toi Toi,1756
305000,Nayland,1757
305100,Aldinga,1758
305200,Victory,1759
305300,Rutherford,1760
305400,Maitlands,1761
305500,Maitai,1762
305600,Grampians,1763
305700,Saxton,1764
305800,Suffolk,1765
305900,Omaio,1766
306000,Enner Glynn,1767
306100,Daelyn,1768
306200,The Brook,1769
363500,Oceanic Nelson Region,1770
306300,Marlborough Sounds West,1771
306400,Marlborough Sounds Coastal Marine,1772
306500,Marlborough Sounds East,1773
306600,Upper Wairau,1774
306700,Waikawa (Marlborough District),1775
306800,Waitohi (Marlborough District),1776
306900,Tuamarina,1777
307000,Awatere,1778
307100,Renwick,1779
307200,Lower Wairau,1780
307300,Woodbourne,1781
307400,Spring Creek-Grovetown,1782
307500,Springlands,1783
307600,Yelverton,1784
307700,Mayfield,1785
307800,Whitney West,1786
307900,Blenheim Central,1787
308000,Riversdale-Islington,1788
308100,Whitney East,1789
308200,Redwoodtown West,1790
308300,Witherlea West,1791
308400,Redwoodtown East,1792
308500,Riverlands,1793
308600,Witherlea East,1794
308700,Inlet Wairau River,1795
363600,Oceanic Marlborough Region,1796
309000,Karamea,1797
309100,Inlets Buller District,1798
309200,Westport North,1799
309300,Westport Rural,1800
309400,Westport South,1801
309500,Buller Coalfields,1802
309600,Charleston (Buller District),1803
309700,Inangahua,1804
309800,Reefton,1805
309900,Barrytown,1806
310000,Runanga,1807
310100,Cobden,1808
310200,Blaketown,1809
310300,Greymouth Central,1810
310400,King Park,1811
310500,Marsden,1812
310600,Karoro,1813
310700,Rutherglen-Camerons,1814
310800,Greymouth Rural,1815
310900,Dobson,1816
311000,Nelson Creek,1817
311100,Lake Brunner,1818
311200,Haast,1819
311300,Westland Glaciers-Bruce Bay,1820
311400,Arahura-Kumara,1821
311500,Hokitika,1822
311600,Inlets Westland District,1823
311700,Hokitika Rural,1824
311800,Waitaha,1825
311900,Whataroa-Harihari,1826
312000,Hokitika Valley-Otira,1827
363400,Oceanic West Coast Region,1828
308800,Kaikoura Ranges,1829
308900,Kaikoura,1830
312100,Hanmer Range,1831
312200,Amuri,1832
312300,Hanmer Springs,1833
312400,Upper Hurunui,1834
312500,Parnassus,1835
312600,Ashley Forest,1836
312700,Omihi,1837
312800,Balcairn,1838
312900,Amberley,1839
313000,Okuku,1840
313100,Ashley Gorge,1841
313200,Oxford,1842
313300,Starvation Hill-Cust,1843
313400,Loburn,1844
313500,Eyrewell,1845
313600,West Eyreton,1846
313700,Ashley-Sefton,1847
313800,Fernside,1848
313900,Rangiora North West,1849
314000,Kingsbury,1850
314100,Ashgrove,1851
314200,Rangiora North East,1852
314300,Oxford Estate,1853
314400,Rangiora Central,1854
314500,Rangiora South West,1855
314600,Lilybrook,1856
314700,Waikuku,1857
314800,Mandeville-Ohoka,1858
314900,Rangiora South East,1859
315000,Southbrook,1860
315100,Swannanoa-Eyreton,1861
315200,Tuahiwi,1862
315300,Woodend,1863
315400,Pegasus,1864
315500,Clarkville,1865
315600,Pegasus Bay,1866
315700,Kaiapoi North West,1867
315800,Silverstream (Waimakariri District),1868
315900,Sovereign Palms,1869
316000,Kaiapoi West,1870
316100,Kaiapoi Central,1871
316200,Kaiapoi South,1872
316300,Kaiapoi East,1873
316400,McLeans Island,1874
316500,Paparua,1875
316600,Yaldhurst,1876
316700,Christchurch Airport,1877
316800,Clearwater,1878
316900,Belfast West,1879
317000,Harewood,1880
317100,Brooklands-Spencerville,1881
317200,Styx,1882
317300,Belfast East,1883
317400,Northwood,1884
317500,Russley,1885
317600,Regents Park,1886
317700,Hawthornden,1887
317800,Bishopdale North,1888
317900,Casebrook,1889
318000,Bishopdale West,1890
318100,Templeton,1891
318200,Islington,1892
318300,Burnside Park,1893
318400,Marshland,1894
318500,Avonhead North,1895
318600,Redwood North,1896
318700,Broomfield,1897
318800,Redwood West,1898
318900,Avonhead West,1899
319000,Bishopdale South,1900
319100,Islington-Hornby Industrial,1901
319200,Burnside,1902
319300,Hei Hei,1903
319400,Papanui North,1904
319500,Avonhead East,1905
319600,Redwood East,1906
319700,Avonhead South,1907
319800,Riccarton Racecourse,1908
319900,Bryndwr North,1909
320000,Northlands (Christchurch City),1910
320100,Papanui West,1911
320200,Ilam North,1912
320300,Hornby West,1913
320400,Hornby Central,1914
320500,Northcote (Christchurch City),1915
320600,Jellie Park,1916
320700,Ilam South,1917
320800,Bryndwr South,1918
320900,Papanui East,1919
321000,Sockburn North,1920
321100,Hornby South,1921
321200,Ilam University,1922
321300,Prestons,1923
321400,Strowan,1924
321500,Fendalton,1925
321600,Waitikiri,1926
321700,Mairehau North,1927
321800,Bush Inn,1928
321900,Awatea North,1929
322000,Upper Riccarton,1930
322100,Malvern,1931
322200,Rutland,1932
322300,Sockburn South,1933
322400,Deans Bush,1934
322500,Wigram North,1935
322600,Holmwood,1936
322700,Wharenui,1937
322800,Wigram West,1938
322900,Awatea South,1939
323000,Merivale,1940
323100,Mairehau South,1941
323200,Mona Vale,1942
323300,Riccarton West,1943
323400,Shirley West,1944
323500,Middleton,1945
323600,Wigram South,1946
323700,Queenspark,1947
323800,St Albans North,1948
323900,St Albans West,1949
324000,Travis Wetlands,1950
324100,Wigram East,1951
324200,Riccarton Central,1952
324300,Oaklands West,1953
324400,Riccarton South,1954
324500,Halswell West,1955
324600,Shirley East,1956
324700,Broken Run,1957
324800,St Albans East,1958
324900,Hagley Park,1959
325000,Hillmorton,1960
325100,Parklands,1961
325200,Riccarton East,1962
325300,Edgeware,1963
325400,Aidanfield,1964
325500,Tower Junction,1965
325600,Burwood,1966
325700,Christchurch Central-West,1967
325800,Christchurch Central-North,1968
325900,Richmond North (Christchurch City),1969
326000,Waimairi Beach,1970
326100,Addington West,1971
326200,Otakaro-Avon River Corridor,1972
326300,Oaklands East,1973
326400,Addington North,1974
326500,Dallington,1975
326600,Christchurch Central,1976
326700,Hoon Hay West,1977
326800,Richmond South (Christchurch City),1978
326900,Spreydon West,1979
327000,Christchurch Central-East,1980
327100,Christchurch Central-South,1981
327200,North Beach,1982
327300,Halswell North,1983
327400,Addington East,1984
327500,Avondale (Christchurch City),1985
327600,Spreydon North,1986
327700,Hoon Hay East,1987
327800,Avonside,1988
327900,Linwood West,1989
328000,Halswell South,1990
328100,Sydenham Central,1991
328200,Spreydon South,1992
328300,Rawhiti,1993
328400,Wainoni,1994
328500,Linwood North,1995
328600,Aranui,1996
328700,Sydenham West,1997
328800,Lancaster Park,1998
328900,Phillipstown,1999
329000,Kennedys Bush,2000
329100,Somerfield East,2001
329200,Somerfield West,2002
329300,Linwood East,2003
329400,Sydenham North,2004
329500,Hoon Hay South,2005
329600,Charleston (Christchurch City),2006
329700,Sydenham South,2007
329800,Bexley,2008
329900,Waltham,2009
330000,Westmorland,2010
330100,Woolston North,2011
330200,New Brighton,2012
330300,Cashmere West,2013
330400,Bromley South,2014
330500,Ensors,2015
330600,Beckenham,2016
330700,Bromley North,2017
330800,St Martins,2018
330900,Opawa,2019
331000,Woolston West,2020
331100,Woolston East,2021
331200,Huntsbury,2022
331300,Cashmere East,2023
331400,Hillsborough (Christchurch City),2024
331500,Woolston South,2025
331600,Port Hills,2026
331700,South New Brighton,2027
331800,Brookhaven-Ferrymead,2028
331900,Heathcote Valley,2029
332000,Mount Pleasant,2030
332100,Redcliffs,2031
332200,Governors Bay,2032
332300,Inlets other Christchurch City,2033
332400,Clifton Hill,2034
332500,Lyttelton,2035
332600,Inlet Port Lyttelton,2036
332700,Sumner,2037
332800,Teddington,2038
332900,Diamond Harbour,2039
333000,Inland water Lake Ellesmere/Te Waihora South,2040
333100,Banks Peninsula South,2041
333200,Eastern Bays-Banks Peninsula,2042
333300,Akaroa Harbour,2043
333400,Inlet Akaroa Harbour,2044
333500,Akaroa,2045
333600,Craigieburn,2046
333700,Torlesse,2047
333800,Glenroy-Hororata,2048
333900,Glentunnel,2049
334000,Darfield,2050
334100,Kirwee,2051
334200,Bankside,2052
334300,Charing Cross,2053
334400,Halkett,2054
334500,Newtons Road,2055
334600,West Melton,2056
334700,Burnham Camp,2057
334800,Rolleston Izone,2058
334900,Rolleston North West,2059
335000,Springston,2060
335100,Rolleston Central,2061
335200,Rolleston North East,2062
335300,Rolleston South West,2063
335400,Southbridge,2064
335500,Rolleston South East,2065
335600,Trents,2066
335700,Prebbleton,2067
335800,Irwell,2068
335900,Ladbrooks,2069
336000,Lincoln West,2070
336100,Lincoln East,2071
336200,Leeston,2072
336300,Tai Tapu,2073
336400,Motukarara,2074
336500,Inland water Lake Ellesmere/Te Waihora North,2075
336600,Ashburton Lakes,2076
336700,Cairnbrae,2077
336800,Ashburton Forks,2078
336900,Methven,2079
337000,Ealing-Lowcliffe,2080
337100,Eiffelton,2081
337200,Chertsey,2082
337300,Winchmore-Wakanui,2083
337400,Allenton North,2084
337500,Allenton South,2085
337600,Rakaia,2086
337700,Ashburton North,2087
337800,Allenton East,2088
337900,Tinwald North,2089
338000,Ashburton Central,2090
338100,Ashburton West,2091
338200,Tinwald South,2092
338300,Ashburton East,2093
338400,Netherby,2094
338500,Hampstead,2095
338600,Ben McLeod,2096
338700,Arundel,2097
338800,Levels Valley,2098
338900,Geraldine,2099
339000,Rangitata,2100
339100,Waitohi (Timaru District),2101
339200,Pleasant Point,2102
339300,Temuka West,2103
339400,Hadlow,2104
339500,Levels,2105
339600,Temuka East,2106
339700,Gleniti North,2107
339800,Washdyke,2108
339900,Fairview,2109
340000,Gleniti South,2110
340100,Glenwood,2111
340200,Marchwiel West,2112
340300,Marchwiel East,2113
340400,Highfield North,2114
340500,Highfield South,2115
340600,Waimataitai-Maori Hill,2116
340700,Fraser Park,2117
340800,Seaview,2118
340900,Inlet Port Timaru,2119
341000,Watlington,2120
341100,Timaru Central,2121
341200,Timaru East,2122
341300,Parkside,2123
341400,Kensington (Timaru District),2124
341500,Mackenzie Lakes,2125
341600,Inland water Lake Pukaki,2126
341700,Inland water Lake Tekapo,2127
341800,Twizel,2128
341900,Opua (Mackenzie District),2129
342000,Fairlie,2130
342100,Hakataramea,2131
342200,Maungati,2132
342300,Lyalldale,2133
342400,Makikihi-Willowbridge,2134
342500,Waimate North,2135
342600,Morven-Glenavy-Ikawai,2136
342700,Waimate West,2137
342800,Waimate East,2138
343100,Aviemore,2139
343200,Inland water Lake Ohau,2140
343300,Danseys Pass,2141
363800,Oceanic Canterbury Region,2142
343400,Ngapara,2143
343500,Lower Waitaki,2144
343600,Waihemo,2145
343700,Maheno,2146
343800,Weston,2147
343900,Oamaru North Milner Park,2148
344000,Oamaru North Orana Park,2149
344100,Oamaru Gardens,2150
344200,Glen Warren,2151
344300,Holmes Hill,2152
344400,Oamaru Central,2153
344500,South Hill,2154
344600,Inlet Port Oamaru,2155
344700,Palmerston,2156
344800,Lindis-Nevis Valleys,2157
344900,Cromwell West,2158
345000,Cromwell East,2159
345100,Manuherikia-Ida Valleys,2160
345200,Earnscleugh,2161
345300,Dunstan-Galloway,2162
345400,Clyde,2163
345500,Alexandra North,2164
345600,Alexandra South,2165
345700,Maniototo,2166
345800,Teviot Valley,2167
345900,Outer Wanaka,2168
346000,Glenorchy,2169
346100,Inland water Lake Wanaka,2170
346200,Outer Wakatipu,2171
346300,Inland water Lake Hawea,2172
346400,Cardrona,2173
346500,Inland water Lake Wakatipu,2174
346600,Wanaka Waterfront,2175
346700,Wanaka North,2176
346800,Wanaka West,2177
346900,Albert Town,2178
347000,Wanaka Central,2179
347100,Lake Hawea,2180
347200,Upper Clutha Valley,2181
347300,Kingston,2182
347400,Arthurs Point,2183
347500,Wakatipu Basin,2184
347600,Queenstown Hill,2185
347700,Warren Park,2186
347800,Sunshine Bay-Fernhill,2187
347900,Arrowtown,2188
348000,Quail Rise,2189
348100,Queenstown Central,2190
348200,Queenstown East,2191
348300,Frankton Arm,2192
348400,Frankton,2193
348500,Lake Hayes,2194
348600,Kelvin Heights,2195
348700,Shotover Country,2196
348800,Lake Hayes Estate,2197
348900,Jacks Point,2198
349000,Strath Taieri,2199
349100,Bucklands Crossing,2200
349200,Waikouaiti,2201
349300,Momona,2202
349400,Taieri,2203
349500,Inlets other Dunedin City,2204
349600,Mount Cargill,2205
349700,Bush Road,2206
349800,Mosgiel East,2207
349900,Mosgiel Central,2208
350000,Seddon Park,2209
350100,Wingatui,2210
350200,Saddle Hill-Chain Hills,2211
350300,East Taieri,2212
350400,Halfway Bush,2213
350500,Helensburgh,2214
350600,Glenleith,2215
350700,Fairfield (Dunedin City),2216
350800,Inlet Otago Harbour,2217
350900,Brockville,2218
351000,Wakari,2219
351100,Abbotsford,2220
351200,Brighton,2221
351300,Pine Hill-Dalmore,2222
351400,Kaikorai-Bradford,2223
351500,Maori Hill,2224
351600,Roslyn (Dunedin City),2225
351700,North East Valley Chingford,2226
351800,Roseneath-Sawyers Bay,2227
351900,Normanby,2228
352000,North East Valley Knox,2229
352100,Belleknowes,2230
352200,Gardens (Dunedin City),2231
352300,Kenmure,2232
352400,Campus West,2233
352500,Waldronville,2234
352600,Green Island,2235
352700,Port Chalmers,2236
352800,Royal Terrace,2237
352900,Arthur Street,2238
353000,Opoho,2239
353100,Campus North,2240
353200,Campus South,2241
353300,Mornington,2242
353400,Dunedin Central,2243
353500,Maryhill,2244
353600,Ravensbourne-St Leonards,2245
353700,Harbourside,2246
353800,Fernhill,2247
353900,Otago Peninsula,2248
354000,Concord,2249
354100,Calton Hill,2250
354200,Caversham,2251
354300,Hillside-Portsmouth Drive,2252
354400,Kew (Dunedin City),2253
354500,Corstorphine,2254
354600,Forbury,2255
354700,Bathgate Park,2256
354800,St Clair,2257
354900,Waverley,2258
355000,Macandrew Bay-Company Bay,2259
355100,Broad Bay-Portobello,2260
355200,St Kilda South,2261
355300,Musselburgh,2262
355400,Shiel Hill,2263
355500,St Kilda North,2264
355600,Andersons Bay,2265
355700,Tainui,2266
355800,West Otago,2267
355900,Tuapeka,2268
356000,Clinton,2269
356100,Clutha Valley,2270
356200,Bruce,2271
356300,Catlins,2272
356400,Milton,2273
356500,Balclutha South,2274
356600,Balclutha North,2275
356700,Benhar-Stirling,2276
356800,Kaitangata-Matau,2277
356900,Inlet Catlins,2278
363900,Oceanic Otago Region,2279
357000,Fiordland,2280
357100,Inlets Fiordland,2281
357200,Inland water Lake Te Anau,2282
357300,Mararoa,2283
357400,Inland water Lake Manapouri,2284
357500,Te Anau,2285
357600,Whitestone,2286
357700,Mossburn,2287
357800,Inland water Lake Hauroko,2288
357900,Longwood Forest,2289
358000,Ohai-Nightcaps,2290
358100,Riversdale-Piano Flat,2291
358200,Lumsden-Balfour,2292
358300,Oreti River,2293
358400,Otautau,2294
358500,Hedgehope,2295
358600,Winton,2296
358700,Waianiwa,2297
358800,Riverton,2298
358900,Wallacetown,2299
359000,Grove Bush,2300
359100,Edendale-Woodlands,2301
359200,Inlets other Southland District,2302
359300,Stewart Island,2303
359400,Awarua Plains,2304
359500,Wyndham-Catlins,2305
359600,Waikaka,2306
359700,Waimumu-Kaiwera,2307
359800,Gore North,2308
359900,Gore West,2309
360000,East Gore,2310
360100,Gore Central,2311
360200,Gore Main,2312
360300,Gore South,2313
360400,Mataura,2314
360500,West Plains-Makarewa,2315
360600,Prestonville-Grasmere,2316
360700,Donovan Park,2317
360800,Myross Bush,2318
360900,Otatara,2319
361000,Invercargill Central,2320
361100,Gladstone (Invercargill City),2321
361200,Rosedale,2322
361300,Avenal,2323
361400,Hargest,2324
361500,Windsor,2325
361600,Richmond (Invercargill City),2326
361700,Glengarry,2327
361800,Inlet New River Estuary,2328
361900,Turnbull Thompson Park,2329
362000,Crinan,2330
362100,Georgetown,2331
362200,Kew (Invercargill City),2332
362300,Kennington-Tisbury,2333
362400,Newfield,2334
362500,Strathern,2335
362600,Elizabeth Park,2336
362700,Aurora,2337
362800,Moulson,2338
362900,Kingswell,2339
363000,Clifton,2340
363100,Woodend-Greenhills,2341
363200,Inlet Bluff Harbour,2342
363300,Bluff,2343
363700,Oceanic Southland Region,2344
258000,Oceanic Three Kings Islands,2345
258100,Three Kings Islands,2346
342900,Oceanic Chatham Islands,2347
343000,Chatham Islands,2348
400001,New Zealand Economic Zone,2349
400002,Oceanic Kermadec Islands,2350
400003,Kermadec Islands,2351
400004,Oceanic Oil Rig Taranaki,2352
400005,Oceanic Campbell Island,2353
400006,Campbell Island,2354
400007,Oceanic Oil Rig Southland,2355
400008,Oceanic Auckland Islands,2356
400009,Auckland Islands,2357
400010,Oceanic Bounty Islands,2358
400011,Bounty Islands,2359
400012,Oceanic Snares Islands,2360
400013,Snares Islands,2361
400014,Oceanic Antipodes Islands,2362
400015,Antipodes Islands,2363
400016,Ross Dependency,2364
DHB01,Northland,2365
DHB02,Waitemata,2366
DHB03,Auckland,2367
DHB04,Counties Manukau,2368
DHB05,Waikato,2369
DHB06,Lakes,2370
DHB07,Bay of Plenty,2371
DHB08,Tairawhiti,2372
DHB09,Taranaki,2373
DHB10,Hawke's Bay,2374
DHB11,Whanganui,2375
DHB12,MidCentral,2376
DHB13,Hutt Valley,2377
DHB14,Capital and Coast,2378
DHB15,Wairarapa,2379
DHB16,Nelson Marlborough,2380
DHB17,West Coast,2381
DHB18,Canterbury,2382
DHB19,South Canterbury,2383
DHB22,Southern,2384
DHB777,Total - District Health Board areas,2385
DHB99,Area Outside District Health Board,2386";

            return fakeFileContents;
        }
    }
}
