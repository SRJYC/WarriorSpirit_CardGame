using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FieldController : MonoBehaviour
{
    public PlayerID m_PlayerID;

    [SerializeField] private FieldBlock m_MiddleFront = null;
    [SerializeField] private FieldBlock m_LeftFront = null;
    [SerializeField] private FieldBlock m_RightFront = null;
    [SerializeField] private FieldBlock m_MiddleBack = null;
    [SerializeField] private FieldBlock m_LeftBack = null;
    [SerializeField] private FieldBlock m_RightBack = null;
    [SerializeField] private FieldBlock m_Center = null;

    public List<FieldBlock> m_blocks { get; private set; }
    private Dictionary<FieldBlockType, FieldBlock[]> m_lines;

    // Start is called before the first frame update
    void Start()
    {
        FieldBlock[] leftBlocks = new FieldBlock[]{ m_LeftFront, m_LeftBack};
        FieldBlock[] middleBlocks = new FieldBlock[] { m_MiddleFront, m_Center, m_MiddleBack };
        FieldBlock[] rightBlocks = new FieldBlock[] { m_RightFront, m_RightBack };
        FieldBlock[] frontBlocks = new FieldBlock[] { m_LeftFront, m_MiddleFront, m_RightFront };
        FieldBlock[] backBlocks = new FieldBlock[] { m_LeftBack, m_MiddleBack, m_RightBack };
        FieldBlock[] center = new FieldBlock[] { m_Center };

        m_lines = new Dictionary<FieldBlockType, FieldBlock[]>();
        m_lines.Add(FieldBlockType.Left, leftBlocks);
        m_lines.Add(FieldBlockType.Middle, middleBlocks);
        m_lines.Add(FieldBlockType.Right, rightBlocks);
        m_lines.Add(FieldBlockType.Front, frontBlocks);
        m_lines.Add(FieldBlockType.Back, backBlocks);
        m_lines.Add(FieldBlockType.Center, center);

        m_blocks = new List<FieldBlock>();
        m_blocks.Add(m_MiddleFront);
        m_blocks.Add(m_LeftFront);
        m_blocks.Add(m_RightFront);
        m_blocks.Add(m_MiddleBack);
        m_blocks.Add(m_LeftBack);
        m_blocks.Add(m_RightBack);
        m_blocks.Add(m_Center);
    }

    public void SetID(PlayerID id)
    {
        m_PlayerID = id;

        m_MiddleFront.m_PlayerID = id;
        m_LeftFront.m_PlayerID = id;
        m_RightFront.m_PlayerID = id;
        m_MiddleBack.m_PlayerID = id;
        m_LeftBack.m_PlayerID = id;
        m_RightBack.m_PlayerID = id;
        m_Center.m_PlayerID = id;
    }

    public List<FieldBlock> GetBlocksOfLine(FieldBlockType line)
    {
        return m_lines[line].ToList();
    }

    public FieldBlock GetBlockByPosition(FieldBlockType row, FieldBlockType column)
    {
        foreach(FieldBlock block in m_blocks)
        {
            if (block.m_RowType == row && block.m_ColumnType == column)
                return block;
        }
        return null;
    }

    public List<FieldBlock> GetBlockBeside(FieldBlock block)
    {
        if (m_PlayerID != block.m_PlayerID)
            return null;

        List<FieldBlock> blocks = new List<FieldBlock>();

        FieldBlockType row = block.m_RowType;

        if(block.m_ColumnType == FieldBlockType.Middle)
        {
            blocks.Add(GetBlockByPosition(row, FieldBlockType.Left));
            blocks.Add(GetBlockByPosition(row, FieldBlockType.Right));
        }
        else
            blocks.Add(GetBlockByPosition(row, FieldBlockType.Middle));

        return blocks;
    }

    public FieldBlock GetCouple(FieldBlock block, bool verticalLine = true)
    {
        if (m_PlayerID != block.m_PlayerID)
            return null;

        //get block behind or in front of given block
        if (verticalLine)
        {
            FieldBlock result = CheckLine(block, FieldBlockType.Front, FieldBlockType.Back);
            if (result != null)
                return result;

            result = CheckLine(block, FieldBlockType.Back, FieldBlockType.Front);
            if (result != null)
                return result;
        }
        //get block on the other side of given block
        else
        {
            FieldBlock result = CheckLine(block, FieldBlockType.Left, FieldBlockType.Right);
            if (result != null)
                return result;

            result = CheckLine(block, FieldBlockType.Right, FieldBlockType.Left);
            if (result != null)
                return result;
        }

        return null;
    }

    private FieldBlock CheckLine(FieldBlock block, FieldBlockType lineToCheck, FieldBlockType lineToReturn)
    {
        FieldBlock[] blocks = m_lines[lineToCheck];
        for(int i=0; i<blocks.Length; i++)
        {
            if (blocks[i] == block)
                return m_lines[lineToReturn][i];
        }
        return null;
    }
}
