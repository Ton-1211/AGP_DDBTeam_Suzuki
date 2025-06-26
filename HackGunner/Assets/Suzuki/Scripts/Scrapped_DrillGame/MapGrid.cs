using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGrid
{
    public enum State
    {
        None = 0,
        RedFloor = 1,
        BlueFloor = 2,
        GreenFloor = 3,
        YellowFloor = 4,
        Max = 5
    }

    public enum Direction
    {
        left = 0,
        right = 1,
        up = 2,
        down = 3
    }

    int x;
    int y;
    State state;
    bool isChecked;
    int connectedNum;
    MapGrid left;
    MapGrid right;
    MapGrid up;
    MapGrid down;

    public MapGrid()
    {
        x = 0;
        y = 0;
        state = State.None;
        connectedNum = 0;
        isChecked = false;
        left = null;
        right = null;
        up = null;
        down = null;
    }
    public MapGrid(int x, int y, State state)
    {
        this.x = x;
        this.y = y;
        this.state = state;
        this.isChecked = false;
        // �㉺���E�ɂȂ����Ă���}�X�͕ʓr�֐���p�ӂ��Čォ��Ȃ���\��
        left = null;
        right = null;
        up = null;
        down = null;
    }

    public int GetX() { return x; }
    public int GetY() { return y; }
    public State GetState() { return state; }

    public MapGrid GetNextGrid(Direction direction)
    {
        switch(direction)
        {
            // return�ŋA��̂�break�͓���Ă��Ȃ�
            case Direction.left: return left;
            case Direction.right: return right;
            case Direction.up: return up;
            case Direction.down: return down;
            default: return null;
        }
    }

    public void Prune()// ���
    {
        state = State.None;// ���̃}�X���󔒂ɂ���
        connectedNum = CheckConnectedNum();

        // �Ȃ����Ă���}�X�̍s����ꏊ�����E�܂ŒT��

        
    }

    public int CheckConnectedNum()
    {
        int sum = 0;
        sum += SearchDirectionConnectedNum(Direction.left);
        sum += SearchDirectionConnectedNum(Direction.right);
        sum += SearchDirectionConnectedNum(Direction.up);
        sum += SearchDirectionConnectedNum(Direction.down);

        return sum;
    }

    private int SearchDirectionConnectedNum(Direction direction)// �w�肵���������󔒂�������܂Œ��ׂ�
    {
        MapGrid next;
        int sum = 0;
        switch(direction)
        {
            case Direction.left:
                while(true)
                {
                    next = left;
                    if(next == null || next.state == State.None)
                    {
                        break;
                    }
                    sum++;
                }
                break;
            case Direction.right:
                while (true)
                {
                    next = right;
                    if (next == null || next.state == State.None)
                    {
                        break;
                    }
                    sum++;
                }
                break;
            case Direction.up:
                while (true)
                {
                    next = up;
                    if (next == null || next.state == State.None)
                    {
                        break;
                    }
                    sum++;
                }
                break;
            case Direction.down:
                while (true)
                {
                    next = down;
                    if (next == null || next.state == State.None)
                    {
                        break;
                    }
                    sum++;
                }
                break;
            default:
                break;
        }
        return sum;
    }
}
