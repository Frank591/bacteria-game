using System.Collections.Generic;

namespace BacteriaSurvive.BL.GridHandlers
{
    public class GridHandlersQueue:IGridHandler
    {
        private IList<IGridHandler> _queue=new List<IGridHandler>();


        public void Add(IGridHandler gridHandler)
        {
            _queue.Add(gridHandler);
        }


        public void ClearQueue()
        {
            _queue.Clear();
        }

        public void Handle(Bacteria[,] grid)
        {
            foreach (IGridHandler gridSaver in _queue)
            {
                gridSaver.Handle(grid);
            }
        }
    }



}