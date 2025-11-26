interface PanelProps {
    text: string,
    items: { id: number; title: string }[];
}

export default function Panel({ text, items }: PanelProps) {
  return (
    <div className="bg-white border border-gray-300 rounded-xl p-3 mt-2">
        <p>{text}</p>
      <ul className="space-y-2">
        {items.map((item) => (
          <li
            key={item.id}
            className="p-2 bg-gray-100 rounded-lg text-gray-800"
          >
            {item.title}
          </li>
        ))}
      </ul>
    </div>
  );
}
