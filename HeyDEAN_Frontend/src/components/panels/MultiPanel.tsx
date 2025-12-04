interface Note {
  NoteId: number;
  Content: string;
  CreatedAt?: string;
}

interface TaskItem {
  TaskId: number;
  Title?: string;
  IsCompleted: boolean;
  CreatedAt?: string;
  DueDate?: string;
}

interface Event {
  EventId: number;
  Title?: string;
  Date?: string;
  StartTime?: string;
  EndTime?: string;
  CreatedAt?: string;
}

interface MultiPanelProps {
  type: 'notes' | 'tasks' | 'events';
  data: Note[] | TaskItem[] | Event[];
  //onClose: () => void;
  onItemAction?: (item: any, action: string) => void;
}

export default function MultiPanel({ type, data, onItemAction }: MultiPanelProps) {
  const getPanelTitle = () => {
    switch (type) {
      case 'notes': return '📝 My Notes';
      case 'tasks': return '✅ My Tasks';
      case 'events': return '📅 My Events';
      default: return 'Panel';
    }
  };

  const renderNoteItem = (note: Note) => (
    <div key={note.NoteId} className="p-3 border border-green-400/30 rounded-lg bg-black/50 backdrop-blur-sm hover:border-green-400/60 transition-all duration-200">
      <p className="text-green-300 font-mono text-sm">{note.Content}</p>
      {note.CreatedAt && (
        <p className="text-xs text-green-500/60 mt-1 font-mono">
          [{new Date(note.CreatedAt).toLocaleDateString()}]
        </p>
      )}
    </div>
  );

  const renderTaskItem = (task: TaskItem) => (
    <div key={task.TaskId} className={`p-3 border rounded-lg bg-black/50 backdrop-blur-sm transition-all duration-200 
      ${
        task.IsCompleted
          ? 'border-green-400/40 opacity-60'
          : 'border-cyan-400/40 hover:border-cyan-400/60'
      }`}>
      <div className="flex items-center justify-between">
        <div className="flex items-center space-x-3">
          <input
            type="checkbox"
            checked={task.IsCompleted}
            onChange={() => onItemAction?.(task, 'toggle')}
            className="w-4 h-4 text-green-400 rounded border-green-400/50 bg-black/50 focus:ring-green-400/50"
          />
          <span className={`font-mono text-sm ${
            task.IsCompleted 
              ? 'line-through text-green-500/50' 
              : 'text-cyan-300' 
          }`}>
            {task.Title || 'UNTITLED_TASK'}
          </span>
        </div>
        {task.DueDate && (
          <span className="text-xs text-green-500/60 font-mono">
            DUE: {new Date(task.DueDate).toLocaleDateString()}
          </span>
        )}
      </div>
    </div>
  );

  const renderEventItem = (event: Event) => (
    <div key={event.EventId} className="p-3 border border-purple-400/40 rounded-lg bg-black/50 backdrop-blur-sm hover:border-purple-400/60 transition-all duration-200">
      <h4 className="font-bold text-purple-300 font-mono">
        {event.Title || 'UNTITLED_EVENT'}
      </h4>
      {event.Date && (
        <div className="text-sm text-purple-400/80 font-mono mt-2 space-y-1">
          <p>📅 {new Date(event.Date).toLocaleDateString()}</p>
          {event.StartTime && event.EndTime && (
            <p>⏰ {event.StartTime} - {event.EndTime}</p>
          )}
        </div>
      )}
    </div>
  );

  const renderContent = () => {
    if (!data || !Array.isArray(data) || data.length === 0) {
      return (
        <div className="text-center py-8 text-gray-500">
          <p className="text-lg">No {type} found</p>
          <p className="text-sm mt-1">Start by adding your first {type.slice(0, -1)}</p>
        </div>
      );
    }

    const items = data as any[];
    
    return (
      <div className="space-y-2 max-h-96 overflow-y-auto">
        {type === 'notes' && items.map((note: Note) => renderNoteItem(note))}
        {type === 'tasks' && items.map((task: TaskItem) => renderTaskItem(task))}
        {type === 'events' && items.map((event: Event) => renderEventItem(event))}
      </div>
    );
  };

  return (
  <div className="fixed inset-0 bg-black/80 backdrop-blur-lg flex items-center justify-center z-50">
    <div className="relative w-full max-w-2xl mx-4 max-h-[80vh] overflow-hidden">
      {/* Glowing border effect */}
      <div className="absolute inset-0 bg-gradient-to-r from-green-400/20 via-cyan-400/20 to-green-400/20 blur-xl"></div>
      
      <div className="relative border border-green-400/50 rounded-xl bg-black/90 backdrop-blur-md shadow-2xl shadow-green-400/20">
        {/* Header */}
        <div className="flex items-center justify-between p-4 border-b border-green-400/30 bg-black/50">
          <h2 className="text-xl font-bold text-transparent bg-clip-text bg-gradient-to-r from-green-400 to-cyan-400 font-mono">
            {getPanelTitle().toUpperCase()}
          </h2>
        </div>
        
        {/* Content */}
        <div className="p-4 max-h-96 overflow-y-auto">
          {renderContent()}
        </div>
      </div>
    </div>
  </div>
);
}
