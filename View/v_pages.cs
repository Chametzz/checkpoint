internal static class View {
    public static Page GetLogin(Window wind) {
        Page mold = new Page(wind, "CHECKPOINT");
        Form login = mold.InsertLabel<Form>("Ingrese sus datos a continuación:");
        login.InsertChild<Input>("Usuario:", ("type", "text"), ("name", "USERNAME"), ("required", "true"));
        login.InsertChild<Input>("Contraseña:", ("type", "password"), ("name", "PASSWORD"), ("required", "true"));
        login.InsertChild<Input>("Iniciar sesión", ("type", "submit"));
        return mold;
    }
    public static Page GetHome(Window wind, Action<Page> action) {
        Page mold = new Page(wind, "EMPLEADO", action);
        mold.InsertLabel<Label>("", ("ref", "welcome"));
        mold.InsertLabel<Label>("", ("ref", "id"));
        mold.InsertLabel<Label>("", ("ref", "first_name"));
        mold.InsertLabel<Label>("", ("ref", "last_name"));
        mold.InsertLabel<Label>("", ("ref", "departament"));
        mold.InsertLabel<Label>("", ("ref", "position"));
        mold.InsertLabel<Button>("Cerrar sesión");
        return mold;
    }
}