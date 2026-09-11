import type { SearchInputProps } from "../types/search";

export function SearchInput({
  value,
  onChange,
  placeholder = "Search...",
}: SearchInputProps) {
  return (
    <div className="flex gap-5 items-center rounded-xl border border-border px-5 py-3">
      <img
        src="/src/assets/magnifier-icon.svg"
        alt=""
        aria-hidden="true"
        className="h-8 w-8"
      />

      <input
        type="text"
        value={value}
        onChange={(event) => onChange(event.target.value)}
        placeholder={placeholder}
        className="text-primary-display-text flex-1 bg-transparent outline-none"
      />
    </div>
  );
}
