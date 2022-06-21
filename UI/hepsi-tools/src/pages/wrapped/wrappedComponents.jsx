import Panel from '../Panel';
import withAuth from "./WithAuth";

const authComponents = {}
const protComponents = {Panel}

const authenticatedComponents = {}
const protectedComponents = {}

for (const [key, value] of Object.entries(authComponents)) {
    authenticatedComponents[`Authenticated${key}`] = withAuth(value)
}

for (const [key, value] of Object.entries(protComponents)) {
    protectedComponents[`Protected${key}`] = withAuth(value)
    console.log(protectedComponents);
}

export const wrappedComponents = {...authenticatedComponents, ...protectedComponents}