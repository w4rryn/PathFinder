using PathFinder;
using Pathfinding.Algorithms;
using Pathfinding.DataStructures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PathFinderGUI
{
    public partial class Form1 : Form
    {
        private PanelGrid gridMaze;
        private readonly Dictionary<CellStates, Color> stateModeColorMap = new Dictionary<CellStates, Color>
        {
            {CellStates.Empty, Color.White },
            {CellStates.Obstacle, Color.Black },
            {CellStates.Start, Color.Green },
            {CellStates.Target, Color.Blue },
            {CellStates.Path, Color.Red },
        };
        private Vertex2D startCell = new Vertex2D(-1, -1);
        private Vertex2D targetCell = new Vertex2D(-1, -1);
        private List<Vertex2D> foundPath = new List<Vertex2D>();

        public Form1()
        {
            InitializeComponent();
            InitializeGridMaze();
            pan_obstacle.BackColor = GetCellColorByMode(CellStates.Obstacle);
            pan_start.BackColor = GetCellColorByMode(CellStates.Start);
            pan_goal.BackColor = GetCellColorByMode(CellStates.Target);
            btn_deletePath.Enabled = false;
        }

        private void InitializeGridMaze()
        {
            var ctx = new PanelGridContext
            {
                GridSize = 15,
                Height = board.Height,
                Width = board.Width,
                CellClickEventHandler = OnCellClickEvent,
                IsWall = CheckIfCellIsWall,
            };
            gridMaze = new PanelGrid(ctx);
            board.Controls.Clear();
            board.Controls.Add(gridMaze);
        }

        private bool CheckIfCellIsWall(Panel cell)
        {
            return cell.BackColor == GetCellColorByMode(CellStates.Obstacle);
        }

        private enum CellStates
        {
            Empty, Start, Target, Obstacle, Path
        }

        private CellStates GetSelectedCellStateMode()
        {
            if (radio_obstacle.Checked)
                return CellStates.Obstacle;
            else if (radio_start.Checked)
                return CellStates.Start;
            else if (radio_target.Checked)
                return CellStates.Target;
            return CellStates.Empty;
        }

        private bool IsCellStartCell(Vertex2D cell)
        {
            return gridMaze.GetCellColorByPosition(cell) == GetCellColorByMode(CellStates.Start);
        }

        private bool IsCellTargetCell(Vertex2D cell)
        {
            return gridMaze.GetCellColorByPosition(cell) == GetCellColorByMode(CellStates.Target);
        }

        private bool IsRightClick(MouseEventArgs e)
        {
            return e.Button == MouseButtons.Right;
        }

        private void OnCellClickEvent(MouseEventArgs e, Vertex2D position)
        {
            if (IsRightClick(e))
                ResetCell(position);
            else
                SetCellColorByMode(position);
        }

        private void ResetCell(Vertex2D cell)
        {
            if (IsCellStartCell(cell))
                startCell = new Vertex2D(-1, -1);
            else if (IsCellTargetCell(cell))
                targetCell = new Vertex2D(-1, -1);
            ResetCellColorAtPosition(cell);
        }

        private void ResetCellColorAtPosition(Vertex2D pos)
        {
            gridMaze.SetCellColorAtPosition(pos, GetCellColorByMode(CellStates.Empty));
        }

        private void SetCellColorByMode(Vertex2D pos)
        {
            CellStates cellMode = GetSelectedCellStateMode();
            UpdateStartOrTargetByMode(pos, cellMode);
            gridMaze.SetCellColorAtPosition(pos, GetCellColorByMode(cellMode));
        }

        private void UpdateStartOrTargetByMode(Vertex2D pos, CellStates cellMode)
        {
            switch (cellMode)
            {
                case CellStates.Start:
                    UpdateStartCellColor(pos);
                    break;
                case CellStates.Target:
                    UpdateTargetCellColor(pos);
                    break;
            }
        }

        private void UpdateStartCellColor(Vertex2D pos)
        {
            if (pos == targetCell)
                targetCell = new Vertex2D(-1, -1);
            if (gridMaze.IsPositionOnGrid(startCell))
                ResetCellColorAtPosition(startCell);
            startCell = pos;
        }

        private void UpdateTargetCellColor(Vertex2D pos)
        {
            if (pos == startCell)
                startCell = new Vertex2D(-1, -1);
            if (gridMaze.IsPositionOnGrid(targetCell))
                ResetCellColorAtPosition(targetCell);
            targetCell = pos;
        }

        private void OnButtonStartSearchClick(object sender, EventArgs e)
        {
            try
            {
                var graph = gridMaze.GetWeightedGraph();
                var star = new Astar<Vertex2D>(graph, ManhattanDistanceHeuristic);
                var path = star.GetPath(new Node<Vertex2D>(startCell, 0), targetCell);
                foundPath = RemoveStartAndGoalFromPath(path);
                RenderGeneratedPath();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            btn_startSearch.Enabled = false;
            btn_deletePath.Enabled = true;
        }

        private List<Vertex2D> RemoveStartAndGoalFromPath(List<Vertex2D> path)
        {
            path.Remove(path.First());
            path.Remove(path.Last());
            return path;
        }

        private void RenderGeneratedPath()
        {
            foreach (var pos in foundPath)
                gridMaze.SetCellColorAtPosition(pos, GetCellColorByMode(CellStates.Path));
        }

        private int ManhattanDistanceHeuristic(Node<Vertex2D> currentLocation, Node<Vertex2D> goalLocation)
        {
            var goal = goalLocation.Position as Vertex2D;
            var current = currentLocation.Position as Vertex2D;
            return Math.Abs(current.X - goal.X) + Math.Abs(current.Y - goal.Y);
        }

        private void OnResetButtonClick(object sender, EventArgs e)
        {
            btn_startSearch.Enabled = true;
            InitializeGridMaze();
        }

        private void OnDeletePathButtonClick(object sender, EventArgs e)
        {
            foreach (var cell in foundPath)
            {
                if (gridMaze.GetCellColorByPosition(cell) == GetCellColorByMode(CellStates.Path))
                    gridMaze.SetCellColorAtPosition(cell, GetCellColorByMode(CellStates.Empty));
            }
            btn_startSearch.Enabled = true;
            btn_deletePath.Enabled = false;
            foundPath.Clear();
        }

        private Color GetCellColorByMode(CellStates state)
        {
            return stateModeColorMap[state];
        }
    }
}