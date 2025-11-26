export default function ChatBubbleDean({ text, panel }: { text: string, panel?: any }) {
  return (
    <div className="flex justify-start">
      <div className="bg-gray-200 text-gray-900 px-4 py-2 rounded-xl max-w-[75%]">
        {text}
        {/* Render panel if it exists */}
        {panel && <div>{}</div>}
      </div>
    </div>
  );
}
