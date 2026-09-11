import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router";
import { FormInput } from "../../../shared/components/FormInput";
import { FormLabel } from "../../../shared/components/FormLabel";
import { FormTitle } from "../../../shared/components/FormTitle";
import { Button } from "../../../shared/components/Button";
import { getUserById, updateUser } from "../api/userApi";
import type { User } from "../types/types";
import type { Role } from "../../roles/types/role";
import { getRoles } from "../../roles/api/roleApi";

export function EditUserPage() {
  const { userId } = useParams();
  const navigate = useNavigate();

  const [user, setUser] = useState<User | null>(null);
  const [roles, setRoles] = useState<Role[]>([]);

  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [email, setEmail] = useState("");
  const [roleId, setRoleId] = useState("");

  useEffect(() => {
    if (!userId) return;

    const fetchData = async () => {
      try {
        const [user, roles] = await Promise.all([
          getUserById(userId),
          getRoles(),
        ]);

        setUser(user);
        setRoles(roles);

        setFirstName(user.firstName);
        setLastName(user.lastName);
        setEmail(user.email);
        setRoleId(user.roleId);
      } catch (error) {
        console.error(error);
      }
    };

    fetchData();
  }, [userId]);

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    if (!userId) return;

    try {
      await updateUser(userId, {
        firstName,
        lastName,
        email,
        roleId,
      });

      navigate("/users");
    } catch (error) {
      console.error(error);
    }
  };

  if (!user) {
    return <p>Loading...</p>;
  }

  return (
    <section className="flex h-full flex-col gap-6 p-6">
      <FormTitle title="EDIT USER" />

      <form onSubmit={handleSubmit} className="flex max-w-xl flex-col gap-5">
        <div>
          <FormLabel htmlFor="firstName" className="text-white">
            First name
          </FormLabel>

          <FormInput
            id="firstName"
            name="firstName"
            value={firstName}
            onChange={(event) => setFirstName(event.target.value)}
          />
        </div>

        <div>
          <FormLabel htmlFor="lastName" className="text-white">
            Last name
          </FormLabel>

          <FormInput
            id="lastName"
            name="lastName"
            value={lastName}
            onChange={(event) => setLastName(event.target.value)}
          />
        </div>

        <div>
          <FormLabel htmlFor="email" className="text-white">
            Email
          </FormLabel>

          <FormInput
            id="email"
            name="email"
            type="email"
            value={email}
            onChange={(event) => setEmail(event.target.value)}
          />
        </div>

        <div>
          <FormLabel htmlFor="role" className="text-white">
            Role
          </FormLabel>

          <select
            id="role"
            name="roleId"
            value={roleId}
            onChange={(event) => setRoleId(event.target.value)}
            className="w-full rounded-md bg-form-input px-4 py-3 text-primary-display-text"
          >
            {roles.map((role) => (
              <option key={role.id} value={role.id}>
                {role.name}
              </option>
            ))}
          </select>
        </div>

        <Button type="submit" variant="form" color="edit">
          Update user
        </Button>
      </form>
    </section>
  );
}
