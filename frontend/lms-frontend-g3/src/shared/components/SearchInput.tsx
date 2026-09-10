import type { SearchInputProps } from "../types/search";

export function SearchInput({
  value,
  onChange,
  placeholder = "Search...",
}: SearchInputProps) {
  return (
    <input
      type="text"
      value={value}
      onChange={(event) => onChange(event.target.value)}
      placeholder={placeholder}
      className="w-full rounded-xl border border-border bg-menu px-4 py-2 text-sm text-primary-display-text outline-none focus:border-[#F0A04B]"
    />
  );
}
