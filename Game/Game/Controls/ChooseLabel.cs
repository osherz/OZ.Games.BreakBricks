using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.Controls
{
    public class ChooseLabel<T> : FlowLayoutPanel
    {
        public event EventHandler<T[]> ItemAdded;
        public event EventHandler<T[]> ItemRemoved;
        public event EventHandler<Size> SizeOfItemChanged;
        public event EventHandler<int> SumItemsOnColumnChanged;
        public event EventHandler<int> SumItemsOnRowChanged;
        public event EventHandler<bool> AutoOrderItemsChanged;
        public event EventHandler<Padding> MarginOfItemsChanged;
        public event EventHandler<ChooseCollection<T>> ItemCollectionChanged;
        public event EventHandler<ChooseCollection<T>> BeforeItemCollectionChanged;
        public event EventHandler<T> ItemClicked;

        private List<Label> itemOptions;
        private ChooseCollection<T> items;
        private Size sizeOfItem;
        private int sumItemsOnColumn;
        private int sumItemsOnRow;
        private bool autoOrderItems = true;
        private bool adminChange = false;
        private Padding marginOfItems;
        private T item;

        public ChooseLabel() : base()
        {
            itemOptions = new List<Label>();
            sizeOfItem = new Size();
            marginOfItems = new Padding();
            Items = new Game.Controls.ChooseCollection<T>();
        }

        protected virtual void EditMainLabel(T item, Label label)
        {
            ///
            ///the 'Tag' of label is will always be the the 'item', you don't need to change it.
            ///
        }

        protected virtual void Items_OnItemRemoved(object sender, CollectionEventArgs<T> e)
        {
            if (IndexOf(e.Item) > -1) RemoveItem(e.Item);
        }

        protected virtual void Items_OnItemAdded(object sender, CollectionEventArgs<T> e)
        {
            if (IndexOf(e.Item) < 0) AddItem(e.Item);
        }

        public void AddItem(params T[] items)
        {
            foreach (T item in items)
            {
                Label label = new Label();
                label.AutoSize = false;
                label.Size = SizeOfItem;
                label.Margin = MarginOfItems;
                EditMainLabel(item, label);
                label.Tag = item;
                AddEventsToItem(label);
                itemOptions.Add(label);
                Controls.Add(label);
                label.Parent = this;
                if (Items.IndexOf(item) == -1)
                {
                    Items.Add(item);
                }
            }
            OnItemAdded(items);
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            if (!e.Control.Equals(itemOptions[itemOptions.Count - 1]))
            {
                Controls.Remove(e.Control);
            }
        }

        public void RemoveItem(params T[] items)
        {
            List<T> removedItems = new List<T>();
            List<Label> labelList = new List<Label>();
            for(int i =0;i<items.Length;i++)
            {
                Label label = labelList.Find((Label temp) => temp.Tag.Equals(items[i]));
                if (label!=null)
                {
                    removedItems.Add((T)label.Tag);
                    label.Parent = null;
                    RemoveEventsFromItem(label);
                    itemOptions.Remove(label);
                    Controls.Remove(label);
                    int index = Items.IndexOf((T)label.Tag);
                    if (index > -1)
                    {
                        Items.RemoveAt(index);
                    }
                }
            }
            if (removedItems.Count > 0) OnItemRemoved(removedItems.ToArray());
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            if (e.Control.Equals(itemOptions[itemOptions.Count - 1]))
            {
                e.Control.Parent = this;
                Controls.Add(e.Control);
            }
        }

        protected virtual void OnItemAdded(T[] items)
        {
            OrderItemsNavigation();
            ItemAdded?.Invoke(this, items);
        }

        protected virtual void OnItemRemoved(T[] items)
        {
            OrderItemsNavigation();
            ItemRemoved?.Invoke(this, items);
        }

        protected virtual void OnItemsCollectionChanged(ChooseCollection<T> items)
        {
            RestartItemsCollectionEvents();
            ItemCollectionChanged?.Invoke(this, items);
        }

        protected virtual void OnBeforeItemsCollectionChanged(ChooseCollection<T> items)
        {
            ResetItemsCollectionEvents();
            BeforeItemCollectionChanged?.Invoke(this, items);
        }

        private void ResetItemsCollectionEvents()
        {
            if (Items != null)
            {
                Items.ItemAdded -= Items_OnItemAdded;
                Items.ItemRemoved -= Items_OnItemRemoved;
            }
        }

        private void RestartItemsCollectionEvents()
        {
            if (Items != null)
            {
                Items.ItemAdded += Items_OnItemAdded;
                Items.ItemRemoved += Items_OnItemRemoved;
            }
        }

        private int IndexOf(T item)
        {
            for(int i =0; i<Controls.Count;i++)
            {
                if (((T)Controls[i].Tag).Equals(item)) return i; 
            }
            return -1;
        }

        public Size SizeOfItem
        {
            get
            {
                return sizeOfItem;
            }

            set
            {
                sizeOfItem = value;
                foreach (Label label in Controls) label.Size = SizeOfItem;
                OnSizeOfItemChanged(sizeOfItem);
            }
        }

        public ChooseCollection<T> Items
        {
            get
            {
                return items;
            }

            set
            {
                OnBeforeItemsCollectionChanged(items);
                items = value;
                OnItemsCollectionChanged(items);
            }
        }

        public T Item
        {
            get
            {
                return item;
            }

            set
            {
                if (IndexOf(value) >= 0)
                    OnItemClicked(value);
            }
        }

        public int SumItemsOnColumn
        {
            get
            {
                return sumItemsOnColumn;
            }

            set
            {
                if (!autoOrderItems && value > 0)
                {
                    SetSumItemsOnColumn(value);
                }
            }
        }

        public int SumItemsOnRow
        {
            get
            {
                return sumItemsOnRow;
            }

            set
            {
                if (!autoOrderItems && value > 0)
                {
                    SetSumItemsOnRow(value);
                }
            }
        }

        public bool AutoOrderItems
        {
            get
            {
                return autoOrderItems;
            }

            set
            {
                autoOrderItems = value;
                OnAutoOrderItemsChanged(autoOrderItems);
            }
        }

        public Padding MarginOfItems
        {
            get
            {
                return marginOfItems;
            }

            set
            {
                marginOfItems = value;
                foreach (Control control in Controls)
                {
                    control.Margin = MarginOfItems;
                }
                OnMargimOfItemsChanged(marginOfItems);
            }
        }

        private void OnMargimOfItemsChanged(Padding marginOfItems)
        {
            OrderItems();
            MarginOfItemsChanged?.Invoke(this, marginOfItems);
        }

        private void OnAutoOrderItemsChanged(bool autoOrderItems)
        {
            if (autoOrderItems)
            {
                OrderItems();
                base.AutoSize = false;
            }
            AutoOrderItemsChanged?.Invoke(this, autoOrderItems);
        }

        private void OnSumItemsOnRowChanged(int sumItemsOnRow)
        {
            OrderItems();
            SumItemsOnRowChanged?.Invoke(this, sumItemsOnRow);
        }

        private void OnSumItemsOnColumnChanged(int sumItemsOnColumn)
        {
            OrderItems();
            SumItemsOnColumnChanged?.Invoke(this, sumItemsOnColumn);
        }

        private void OnSizeOfItemChanged(Size sizeOfItem)
        {
            OrderItems();
            SizeOfItemChanged?.Invoke(this, sizeOfItem);
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            if (AutoOrderItems && !adminChange)
            {
                OrderItems();
            }
        }

        protected override void OnAutoSizeChanged(EventArgs e)
        {
            base.OnAutoSizeChanged(e);
            if (AutoSize)
            {
                autoOrderItems = false;
            }
        }

        private void OrderItems()
        {
            int width = 0;
            int height = 0;
            int sumOnRow = SumItemsOnRow;
            if (SumItemsOnColumn > 0)
            {
                sumOnRow = Controls.Count / SumItemsOnColumn;
                if (sumOnRow == 0) sumOnRow = 1;
            }

            for (int i = 0; i < Controls.Count; i++)
            {
                if (i < sumOnRow)
                {
                    width += Controls[i].Margin.Left + Controls[i].Width + Controls[i].Margin.Right;
                }
                if (i % sumOnRow == 0)
                {
                    height += Controls[i].Margin.Top + Controls[i].Height + Controls[i].Margin.Bottom;
                }
            }
            adminChange = true;
            Width = width;
            Height = height;
            adminChange = false;
        }

        private void OrderSumItemsOnColumn()
        {
            SetSumItemsOnColumn((int)Math.Sqrt(Controls.Count));
        }

        private void OrderItemsNavigation()
        {
            if (autoOrderItems)
            {
                OrderSumItemsOnColumn();
            }
            else
            {
                OrderItems();
            }
        }

        private void SetSumItemsOnColumn(int sum)
        {
            sumItemsOnColumn = sum;
            sumItemsOnRow = -1;
            OnSumItemsOnColumnChanged(sum);
        }

        private void SetSumItemsOnRow(int sum)
        {
            sumItemsOnRow = sum;
            sumItemsOnColumn = -1;
            OnSumItemsOnRowChanged(sum);
        }

        private void Item_OnMouseEnter(object sender, EventArgs e)
        {
            Label chooseLabel = (Label)sender;
            if (!Item.Equals(chooseLabel.Tag)) DesiegnOfLabelWhenMouseEnter(chooseLabel);
            foreach (Label label in itemOptions)
            {
                if (!Item.Equals((T)label.Tag) && !label.Tag.Equals(chooseLabel.Tag))
                {
                    DesiegnOfNotChooseLabel(label);
                }
            }
        }

        private void Item_OnMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                OnItemClicked((T)((Label)sender).Tag);
            }
        }

        protected virtual void OnItemClicked(T item)
        {
            this.item = item;
            foreach (Label label in itemOptions)
            {
                if (!Item.Equals((T)label.Tag))
                {
                    DesiegnOfNotChooseLabel(label);
                }
                else
                {
                    DesiegnOfChooseLabel(label);
                }
            }
            ItemClicked?.Invoke(this, item);
        }

        protected virtual void DesiegnOfLabelWhenMouseEnter(Label chooseLabel)
        {
            DesiegnOfChooseLabel(chooseLabel);
        }

        protected virtual void DesiegnOfChooseLabel(Label chooseLabel)
        {
            chooseLabel.BorderStyle = BorderStyle.FixedSingle;
        }

        protected virtual void DesiegnOfNotChooseLabel(Label notChooseLabel)
        {
            notChooseLabel.BorderStyle = BorderStyle.None;
        }

        private void Item_OnMouseLeave(object sender, EventArgs e)
        {
            Label itemLabel = ((Label)sender);
            if (!Item.Equals((T)itemLabel.Tag)) DesiegnOfNotChooseLabel(itemLabel);
        }

        private void RemoveEventsFromItem(Label itemLabel)
        {
            if (itemLabel != null)
            {
                itemLabel.MouseClick -= Item_OnMouseClick;
                itemLabel.MouseEnter -= Item_OnMouseEnter;
                itemLabel.MouseLeave -= Item_OnMouseLeave;
            }

        }

        private void AddEventsToItem(Label itemLabel)
        {
            if (itemLabel != null)
            {
                itemLabel.MouseClick += Item_OnMouseClick;
                itemLabel.MouseEnter += Item_OnMouseEnter;
                itemLabel.MouseLeave += Item_OnMouseLeave;
            }
        }


    }
}
