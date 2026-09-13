import { DisplayText } from "../shared/components/DisplayText";
import { useAuth } from "./auth/AuthContext";
import { GreetingText } from "../shared/components/GreetingText";

export function MainPage() {
  const { user } = useAuth();

  return (
    <section className="flex h-full gap-6 p-6">
      <div className="flex flex-col gap-5">
        <DisplayText text="DASHBOARD" />

        <GreetingText name={user?.firstName} />
      </div>
    </section>
  );
}
