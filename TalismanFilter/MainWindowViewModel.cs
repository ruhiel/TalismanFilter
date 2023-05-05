using Csv;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.WindowsAPICodePack.Dialogs;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalismanFilter
{
    public class MainWindowViewModel
    {
        public ReactiveCommand FileOpenCommand { get; } = new ReactiveCommand();
        public ReactiveCommand OutputCommand { get; } = new ReactiveCommand();
        public IDialogCoordinator? MahAppsDialogCoordinator { get; set; }
        public ObservableCollection<LineInfo> LineInfoList { get; } = new ObservableCollection<LineInfo>();

        public ObservableCollection<LineInfo> FilterLineInfoList { get; } = new ObservableCollection<LineInfo>();
        #region チェックボックス

        #endregion

        #region 除外チェックボックス
        public ReactiveProperty<bool> SpareShotExclude { get; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> BallisticsExclude { get; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> BowChargePlusExclude { get; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> SpecialAmmoBoostExclude { get; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> NormalRapidUpExclude { get; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> PierceUpExclude { get; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> SpreadUpExclude { get; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> AmmoUpExclude { get; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> ReloadSpeedExclude { get; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> RecoilDownExclude { get; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> SteadinessExclude { get; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> RapidFireUpExclude { get; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> TuneUpExclude { get; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> MastersTouchExclude { get; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> HandicraftExclude { get; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> RazorSharpExclude { get; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> ProtectivePolishExclude { get; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> MindsEyeExclude { get; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> SpeedSharpeningExclude { get; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> Grinder_SExclude { get; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> BladescaleHoneExclude { get; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> EarplugsExclude { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> WindproofExclude { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> TremorResistanceExclude { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> BubblyDanceExclude { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> PoisonResistanceExclude { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> ParalysisResistanceExclude { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> SleepResistanceExclude { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> StunResistanceExclude { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> MuckResistanceExclude { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> BlastResistanceExclude { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> FlinchFreeExclude { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> DefianceExclude { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> IntrepidHeartExclude { get; } = new ReactiveProperty<bool>(true);

        public ReactiveProperty<bool> FireResistanceExclude { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> WaterResistanceExclude { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> IceResistanceExclude { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> ThunderResistanceExclude { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> DragonResistanceExclude { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> BlightResistanceExclude { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> SpeedEatingExclude { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> BombardierExclude { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> MushroomancerExclude { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> ItemProlongerExclude { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> WideRangeExclude { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> FreeMealExclude { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> BotanistExclude { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> GeologistExclude { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> HungerResistanceExclude { get; } = new ReactiveProperty<bool>(true);

        #endregion
        public MainWindowViewModel()
        {
            var path = TalismanFilter.Properties.Settings.Default.DefaultFilePath;

            var filePath = string.IsNullOrEmpty(path) ? @"C:\Users\Public" : Path.GetDirectoryName(path);

            var fileName = string.IsNullOrEmpty(path) ? "Save000_ExportedTalismans.txt" : Path.GetFileName(path);

            FileOpenCommand.Subscribe(e =>
            {
                using (var cofd = new CommonOpenFileDialog()
                {
                    Title = "ファイルを選択してください",
                    InitialDirectory = filePath,
                })
                {
                    cofd.DefaultFileName = fileName;

                    if (cofd.ShowDialog() == CommonFileDialogResult.Ok)
                    {

                        Properties.Settings.Default.DefaultFilePath = cofd.FileName;
                        Properties.Settings.Default.Save();
                        // ファイルの全内容を文字列に読込みます
                        string csv = File.ReadAllText(cofd.FileName);
                        var options = new CsvOptions();
                        options.HeaderMode = HeaderMode.HeaderAbsent;
                        // ファイルの1行分を2次元配列として取得します
                        foreach (ICsvLine line in CsvReader.ReadFromText(csv, options))
                        {
                            var skill1 = line[0];
                            var skill1Level = line[1];
                            var skill2 = line[2];
                            var skill2Level = line[3];
                            var slot1 = line[4];
                            var slot2 = line[5];
                            var slot3 = line[6];
                            var info = new LineInfo(skill1, skill1Level, skill2, skill2Level, slot1, slot2, slot3);

                            LineInfoList.Add(info);
                        }

                        Filter();
                    }
                }
            });

            OutputCommand.Subscribe(e =>
            {
                using (var cofd = new CommonOpenFileDialog()
                {
                    Title = "ファイルを選択してください",
                    InitialDirectory = filePath,
                })
                {
                    if (cofd.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        var rows = new List<string[]>();
                        foreach (var item in FilterLineInfoList)
                        {
                            var row = new[] { item.Skill1, item.Skill1Level.ToString(), item.Skill2, item.Skill2Level.ToString(), item.Slot1.ToString(), item.Slot2.ToString(), item.Slot3.ToString() };

                            rows.Add(row);
                        }
                        var csv = CsvWriter.WriteToText(new string[] { "Skill1", "Skill1Level", "Skill2", "Skill2Level", "Slot1", "Slot2", "Slot3" }, rows, skipHeaderRow: true);
                        File.WriteAllText(cofd.FileName, csv);
                    }
                }

            });
        }
        private void Filter()
        {
            FilterLineInfoList.Clear();

            var skillList = new List<Tuple<string, string>>()
            {
                Tuple.Create("弾丸節約", "SpareShot"),
                Tuple.Create("弾道強化", "Ballistics"),
                Tuple.Create("弓溜め段階解放", "BowChargePlus"),
                Tuple.Create("特殊射撃強化", "SpecialAmmoBoost"),
                Tuple.Create("通常弾・連射矢強化", "NormalRapidUp"),
                Tuple.Create("貫通弾・貫通矢強化", "PierceUp"),
                Tuple.Create("散弾・拡散矢強化", "SpreadUp"),
                Tuple.Create("装填拡張", "AmmoUp"),
                Tuple.Create("装填速度", "ReloadSpeed"),
                Tuple.Create("反動軽減", "RecoilDown"),
                Tuple.Create("ブレ抑制", "Steadiness"),
                Tuple.Create("速射強化", "RapidFireUp"),
                Tuple.Create("チューンアップ", "TuneUp"),

                Tuple.Create("達人芸", "MastersTouch"),
                Tuple.Create("匠", "Handicraft"),
                Tuple.Create("業物", "RazorSharp"),
                Tuple.Create("剛刃研磨", "ProtectivePolish"),
                Tuple.Create("心眼", "MindsEye"),
                Tuple.Create("砥石使用高速化", "SpeedSharpening"),
                Tuple.Create("研磨術【鋭】", "Grinder_S"),
                Tuple.Create("刃鱗磨き", "BladescaleHone"),

                Tuple.Create("耳栓", "Earplugs"),
                Tuple.Create("風圧耐性", "Windproof"),
                Tuple.Create("耐震", "TremorResistance"),
                Tuple.Create("泡沫の舞", "BubblyDance"),
                Tuple.Create("毒耐性", "PoisonResistance"),
                Tuple.Create("麻痺耐性", "ParalysisResistance"),
                Tuple.Create("睡眠耐性", "SleepResistance"),
                Tuple.Create("気絶耐性", "StunResistance"),
                Tuple.Create("泥雪耐性", "MuckResistance"),
                Tuple.Create("爆破やられ耐性", "BlastResistance"),
                Tuple.Create("ひるみ軽減", "FlinchFree"),
                Tuple.Create("顕如盤石", "Defiance"),
                Tuple.Create("剛心", "IntrepidHeart"),

                Tuple.Create("火耐性", "FireResistance"),
                Tuple.Create("水耐性", "WaterResistance"),
                Tuple.Create("氷耐性", "IceResistance"),
                Tuple.Create("雷耐性", "ThunderResistance"),
                Tuple.Create("龍耐性", "DragonResistance"),
                Tuple.Create("属性やられ耐性", "BlightResistance"),

                Tuple.Create("早食い", "SpeedEating"),
                Tuple.Create("砥石使用高速化", "SpeedSharpening"),
                Tuple.Create("ボマー", "Bombardier"),
                Tuple.Create("キノコ大好き", "Mushroomancer"),
                Tuple.Create("アイテム使用強化", "ItemProlonger"),
                Tuple.Create("広域化", "WideRange"),
                Tuple.Create("満足感", "FreeMeal"),

                Tuple.Create("植生学", "Botanist"),
                Tuple.Create("地質学", "Geologist"),
                Tuple.Create("腹減り耐性", "HungerResistance"),
            };

            foreach (var lineinfo in LineInfoList)
            {
                var exclude = false;
                // 除外
                foreach (var item in skillList)
                {
                    var property = GetType().GetProperty($"{item.Item2}Exclude");
                    if (property == null) continue;
                    var reactiveProperty = property.GetValue(this) as ReactiveProperty<bool>;
                    if (reactiveProperty == null) continue;
                    if (!reactiveProperty.Value) continue;

                    if (lineinfo.Skill1.Equals(item.Item1) || lineinfo.Skill2.Equals(item.Item1))
                    {
                        exclude = true;
                        break;
                    }
                }

                if(exclude)
                {
                    continue;
                }

                FilterLineInfoList.Add(lineinfo);


                /*
                foreach (var item in skillList)
                {
                    var property = GetType().GetProperty(item.Item2);
                    if (property == null) continue;
                    var reactiveProperty = property.GetValue(this) as ReactiveProperty<bool>;
                    if (reactiveProperty == null) continue;
                    if (!reactiveProperty.Value) continue;

                    if (lineinfo.Skill1.Equals(item.Item1) || lineinfo.Skill2.Equals(item.Item1))
                    {
                        FilterLineInfoList.Add(lineinfo);
                        break;
                    }
                }*/
            }
        }
    }
}
