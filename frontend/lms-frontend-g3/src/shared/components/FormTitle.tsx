interface FormTitleProps {
  title: string;
}

export function FormTitle({ title }: FormTitleProps) {
  return <p className=" text-3xl text-primary-title-text bg-menu">{title}</p>;
}
