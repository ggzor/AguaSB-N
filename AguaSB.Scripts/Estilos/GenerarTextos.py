plantilla = """
\t<Style x:Key="{tono}-{peso}{tamaño}" TargetType="TextBlock" BasedOn="{{StaticResource {tono}-{peso}}}">
\t\t<Setter Property="FontSize" Value="{{StaticResource t{tamaño}}}"/>
\t</Style>"""

def crearCodigo(tono, peso, tamaño):
    return plantilla.format(**locals())

def generar(tonos, pesos, tamaños):
    for t in tonos:
        for p in pesos:
            for h in tamaños:
                yield crearCodigo(t, p, h)

tonos = ["w", "b"]
pesos = ["l", "n", "h", "u"]
tamaños = list(map(str, range(1, 7)))

resultado = list(generar(tonos, pesos, tamaños))

for r in resultado:
    print(r)