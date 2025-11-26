export default function ChatBubbleUser({ text }: { text: string }) {
  return (
    <div className="flex justify-end">
      <div className="bg-blue-600 text-white px-4 py-2 rounded-xl max-w-[75%]">
        {text}
      </div>
    </div>
  );
}
