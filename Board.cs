﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLogic
{
    public class Board
    {
        private readonly Piece[,] pieces = new Piece[8, 8];

        public Piece this[int row, int col]
        {
            get { return pieces[row, col]; }
            set { pieces[row, col] = value; }
        }

        public Piece this[Position pos]
        {
            get { return this[pos.Row, pos.Column]; }
            set { this[pos.Row, pos.Column] = value; }
        }

        public static Board Initial()
        {
            Board board = new Board();
            board.AddStartPiece();
            return board;
        }

        private void AddStartPiece()
        {
            this[0, 0] = new Rook(Player.Black);
            this[0, 1] = new Knight(Player.Black);
            this[0, 2] = new Bishop(Player.Black);
            this[0, 3] = new Queen(Player.Black);
            this[0, 4] = new King(Player.Black);
            this[0, 5] = new Bishop(Player.Black);
            this[0, 6] = new Knight(Player.Black);
            this[0, 7] = new Rook(Player.Black);



            this[7, 0] = new Rook(Player.white);
            this[7, 1] = new Knight(Player.white);
            this[7, 2] = new Bishop(Player.white);
            this[7, 3] = new Queen(Player.white);
            this[7, 4] = new King(Player.white);
            this[7, 5] = new Bishop(Player.white);
            this[7, 6] = new Knight(Player.white);
            this[7, 7] = new Rook(Player.white);

            for (int i = 0; i <= 7; i++)
            {
                this[1, i] = new Pawn(Player.Black);
                this[6, i] = new Pawn(Player.white);
            }
        }
        public static bool IsInside(Position pos)
        {
            return pos.Row >= 0 && pos.Row < 8 && pos.Column >= 0 && pos.Column < 8;
        }
        public bool IsEmpty(Position pos)
        {
            return this[pos] == null;
        }

        public IEnumerable<Position> PiecePositions()
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Position pos = new Position(r, c);
                    if (!IsEmpty(pos))
                    {
                        yield return pos;
                    }
                }
            }
        }
        public IEnumerable<Position> PiecePositionsFor(Player player)
        {
            return PiecePositions().Where(pos => this[pos].Color == player);
        }
        public bool IsInCheck(Player player)
        {
            return PiecePositionsFor(player.Opponent()).Any(pos =>
            {
                Piece piece = this[pos];
                return piece.CanCaptureKing(pos,this);
            });
        }

        public Board Copy()
        {
            Board copy = new Board();
            foreach (Position pos in PiecePositions()) {
                copy[pos] = this[pos].Copy();
            }
            return copy;
        }
    } 
}
