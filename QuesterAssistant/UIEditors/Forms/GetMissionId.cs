using MyNW.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using MyNW.Internals;

namespace QuesterAssistant.UIEditors.Forms
{
    public partial class GetMissionId : XtraForm
    {
        private Dictionary<TreeNode, string> missionsNodes = new Dictionary<TreeNode, string>();
        private string specificMission = string.Empty;
        private bool onlyMain;
        private bool localizedNameOnly;
        private bool valid;

        public GetMissionId()
        {
            InitializeComponent();
        }

        private void addChildrens(Mission mission, TreeNode parentNode, string currentPath)
        {
            foreach (var mission2 in mission.Childrens)
            {
                var text = $"{currentPath}/{mission2.MissionName}";
                var text2 = $"{mission2.UIStringMsg}  [{mission2.MissionName}]";
                var treeNode = parentNode.Nodes.Add($"{text2} : {mission2.State}");
                missionsNodes.Add(treeNode, text);
                addChildrens(mission2, treeNode, text);
            }
        }

        private void GetMissionId_Load(object sender, EventArgs e)
        {
            refreshList();
            if (specificMission.Length > 0)
            {
                rgMissionType.SelectedIndex = 1;
                var key = missionsNodes.FirstOrDefault(mn => mn.Value == specificMission).Key;
                if (key != null)
                {
                    treeMissions.SelectedNode = key;
                    return;
                }
                rgMissionType.SelectedIndex = 2;
                key = missionsNodes.FirstOrDefault(mn => mn.Value == specificMission).Key;
                if (key != null)
                {
                    treeMissions.SelectedNode = key;
                    return;
                }
                rgMissionType.SelectedIndex = 0;
                key = missionsNodes.FirstOrDefault(mn => mn.Value == specificMission).Key;
                if (key != null)
                {
                    treeMissions.SelectedNode = key;
                }
            }
        }

        public static string Show(bool onlyMain = false, string specificMission = "", bool localizedNameOnly = false, bool onlyOpen = false)
        {
            var getMissionId = new GetMissionId
            {
                localizedNameOnly = localizedNameOnly,
                onlyMain = onlyMain,
                specificMission = specificMission
            };
            if (onlyOpen)
            {
                getMissionId.rgMissionType.SelectedIndex = 1;
                getMissionId.rgMissionType.Properties.Items[0].Enabled = false;
                getMissionId.rgMissionType.Properties.Items[2].Enabled = false;
            }
            getMissionId.ShowDialog();
            if (getMissionId.valid && getMissionId.treeMissions.SelectedNode != null && getMissionId.missionsNodes.ContainsKey(getMissionId.treeMissions.SelectedNode))
            {
                return getMissionId.missionsNodes[getMissionId.treeMissions.SelectedNode];
            }
            return string.Empty;
        }

        private void b_Select_Click(object sender, EventArgs e)
        {
            valid = true;
            Close();
        }

        private void refreshActive(bool repeatable = false)
        {
            missionsNodes.Clear();
            treeMissions.Nodes.Clear();
            var missions = EntityManager.LocalPlayer.Player.MissionInfo.Missions;
            if (rgSortBy.SelectedIndex == 0)
            {
                missions = missions.Where(m => !string.IsNullOrEmpty(m.MissionDef.DisplayName))
                    .OrderBy(m => m.MissionDef.DisplayName).ToList();
            }

            foreach (var mission in missions)
            {
                if (repeatable && !mission.MissionDef.CanRepeat)
                    continue;
                if (mission.MissionDef.DisplayName.Length > 0 && mission.MissionDef.MissionType == 0)
                {
                    var text = rgSortBy.SelectedIndex == 0
                        ? $"{mission.MissionDef.DisplayName} [{mission.MissionDef.Name}]"
                        : $"{mission.MissionDef.Name} ({mission.MissionDef.DisplayName})";
                    text += $" : {mission.State}";

                    var treeNode = treeMissions.Nodes.Add(text);
                    missionsNodes.Add(treeNode, mission.MissionName);
                    if (!onlyMain)
                        addChildrens(mission, treeNode, mission.MissionName);
                }
            }
        }

        private void refreshCompleted()
        {
            missionsNodes.Clear();
            treeMissions.Nodes.Clear();
            var missions = EntityManager.LocalPlayer.Player.MissionInfo.CompletedMissions;
            if (rgSortBy.SelectedIndex == 0)
            {
                missions = missions.Where(m => !string.IsNullOrEmpty(m.MissionDef.DisplayName))
                    .OrderBy(m => m.MissionDef.DisplayName).ToList();
            }

            foreach (var mission in missions)
            {
                if (mission.MissionDef.Name.Length > 0 && mission.MissionDef.MissionType == 0)
                {
                    var text = rgSortBy.SelectedIndex == 0
                        ? $"{mission.MissionDef.DisplayName} [{mission.MissionDef.Name}]"
                        : $"{mission.MissionDef.Name} ({mission.MissionDef.DisplayName})";

                    var treeNode = treeMissions.Nodes.Add(text);
                    missionsNodes.Add(treeNode, mission.MissionDef.Name);
                    if (!onlyMain)
                        addSubMissions(mission.MissionDef, treeNode, mission.MissionDef.Name);
                }
            }
        }

        private void refreshOpen()
        {
            missionsNodes.Clear();
            treeMissions.Nodes.Clear();
            var missions = EntityManager.LocalPlayer.MapState.OpenMissions;
            if (rgSortBy.SelectedIndex == 0)
            {
                missions = missions.Where(m => !string.IsNullOrEmpty(m.Mission.MissionDef.DisplayName))
                    .OrderBy(m => m.Mission.MissionDef.DisplayName).ToList();
            }

            foreach (var mission in missions)
            {
                if (mission.Mission.MissionDef.Name.Length > 0)
                {
                    var text = rgSortBy.SelectedIndex == 0
                        ? $"{mission.Mission.MissionDef.DisplayName} [{mission.Mission.MissionDef.Name}]"
                        : $"{mission.Mission.MissionDef.Name} ({mission.Mission.MissionDef.DisplayName})";
                    text += $" : {mission.Mission.State}";
                    var treeNode = treeMissions.Nodes.Add(text);
                    missionsNodes.Add(treeNode, mission.Mission.MissionDef.Name);
                    if (!onlyMain)
                        addChildrens(mission.Mission, treeNode, mission.Mission.MissionDef.Name);
                }
            }
        }
        
        private void addSubMissions(MissionDef missionDef, TreeNode parentNode, string currentPath)
        {
            foreach (var missionDef2 in missionDef.SubMissions)
            {
                var text = $"{currentPath}/{missionDef2.Name}";
                var text2 = $"{missionDef2.UIStringMsg} [{missionDef2.Name}]";
                var treeNode = parentNode.Nodes.Add(text2);
                missionsNodes.Add(treeNode, text);
                addSubMissions(missionDef2, treeNode, text);
            }
        }
        
        private void refreshList()
        {
            switch (rgMissionType.SelectedIndex)
            {
                case 0:
                    refreshActive();
                    return;
                case 1:
                    refreshOpen();
                    return;
                case 2:
                    refreshCompleted();
                    return;
                case 3:
                    refreshActive(true);
                    return;
                default:
                    return;
            }
        }

        private void b_Refresh_Click(object sender, EventArgs e) => refreshList();
        private void rgMissionType_SelectedIndexChanged(object sender, EventArgs e) => refreshList();
        private void rgSortBy_SelectedIndexChanged(object sender, EventArgs e) => refreshList();
    }
}